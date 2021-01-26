using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Mine.Models;
using Mine.Views;

namespace Mine.ViewModels
{
    public class ItemIndexViewModel : BaseViewModel
    {
        public ObservableCollection<ItemModel> DataSet { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemIndexViewModel()
        {
            Title = "Items";
            DataSet = new ObservableCollection<ItemModel>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<ItemCreatePage, ItemModel>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as ItemModel;
                DataSet.Add(newItem);
                await DataStore.CreateAsync(newItem);
            });

            MessagingCenter.Subscribe<ItemDeletePage, ItemModel>(this, "DeleteItem", async (obj, item) =>
            {
                var data = item as ItemModel;
             
                await DeleteAsync(data);
            });

            MessagingCenter.Subscribe<ItemUpdatePage, ItemModel>(this, "UpdateItem", async (obj, item) =>
            {
                var data = item as ItemModel;

                await UpdateAsync(data);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DataSet.Clear();
                var items = await DataStore.IndexAsync(true);
                foreach (var item in items)
                {
                    DataSet.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<ItemModel> ReadAsync(string id)
        {
            var result = await DataStore.ReadAsync(id);

            return result;
        }

        /// <summary>
        /// Delete item record in the system
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync (ItemModel data)
        {
            //Confirm data record exists. If not return null
            var record = await ReadAsync(data.Id);
            if (record == null) { return false; }

            //Remove from local data set cache
            DataSet.Remove(data);

            //Call to remove from datastore
            var result = await DataStore.DeleteAsync(data.Id);

            return result;
        }

        /// <summary>
        /// Update Item record in the system
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ItemModel data)
        {
            //Confirm data record exists. If not return null
            var record = await ReadAsync(data.Id);
            if (record == null) { return false; }

            //Call to update in datastore
            var result = await DataStore.UpdateAsync(data);

            var canExecute = LoadItemsCommand.CanExecute(null);
            LoadItemsCommand.Execute(null);

            return result;
        }
    }
}