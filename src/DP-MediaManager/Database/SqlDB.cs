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

        public Boolean Add(IMovie movie, ISeries series)
        {
            return true;
        }

        public void Update()
        {

        }

        public void Search(String searchItem)
        {

        }

        public Boolean Remove()
        {
            return true;
        }

        public List<Entry> GetAll()
        {
            return null;
        }

        public Entry Get()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Entry>("SELECT * FROM entry", new DynamicParameters());
                return output.ToList();
            }
        }
    }
}
