using Dapper;
using DP_MediaManager.LibraryItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.Database
{
    class SqlDB : IDatabase
    {
        public string LoadConnectionString(String id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public Boolean Add(LibraryItem.LibraryFactory item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                if (item != null)
                {
                    //Movie
                    cnn.Execute("INSERT INTO values ", item);
                    return true;
                }
                else
                {
                    //Series
                    cnn.Execute("INSERT INTO values ", item);
                    return true;
                }
            }
        }

        public Boolean Update(LibraryItem.LibraryFactory item)
        {
            return true;
        }

        public List<LibraryItem.LibraryFactory> Search(String searchItem)
        {
            return null;
        }

        public Boolean Remove(LibraryItem.LibraryFactory item)
        {
            return true;
        }

        public List<LibraryItem.LibraryFactory> GetAll()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Entry>("SELECT * FROM entry", new DynamicParameters());
                //return output.ToList();
                return null;
            }
        }
    }
}
