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

        public void Add(LibraryItem.LibraryFactory item)
        {

        }

        public void Update(LibraryItem.LibraryFactory item)
        {

        }

        public List<LibraryItem.LibraryFactory> Search(String searchItem)
        {
            return null;
        }

        public void Remove(LibraryItem.LibraryFactory item)
        {

        }

        public List<LibraryItem.LibraryFactory> GetAll()
        {
            return null;
        }
    }
}
