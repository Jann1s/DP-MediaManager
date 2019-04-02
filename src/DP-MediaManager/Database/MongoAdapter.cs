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
    class MongoAdapter : IDatabase
    {
        private MongoDB Mongo;

        public string LoadConnectionString(String id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public Boolean Add(LibraryItem.LibraryFactory item)
        {
            return true;
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
            return null;
        }
    }
}
