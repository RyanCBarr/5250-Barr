﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine.Models;

namespace Mine.Services
{
    public class MockDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Wooden Sword", Description="Basic training Sword made from wood.", Value=1 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Iron Sword", Description="Mass Produced and dependable.", Value=2 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Steel Sword", Description="Forged by a master, superior edge and durability.", Value=4 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Mithril Sword", Description="Made from magicly embued ore from deep in the dwarven forges.", Value=6 },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Thunderfurry, Blessed Blade of the Windseeker", Description="Forged for the elemental gods themselves, not intended for mortal hands.", Value=10 }
            };
        }

        public async Task<bool> AddItemAsync(ItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ItemModel item)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}