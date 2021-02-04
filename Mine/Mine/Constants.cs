using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Mine
{
    public static class Constants
    {
        //public const string DatabaseFilename = "TodoSQLite.db3";
        public const string DatabaseFilename = "mine.db3";


        public const SQLite.SQLiteOpenFlags Flags =

            //Open DB in Read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |

            //Create DB is it doesn't exist
            SQLite.SQLiteOpenFlags.Create |

            //Enable Multi0threaded DB access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

    }
}
