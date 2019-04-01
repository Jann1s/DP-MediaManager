using DP_MediaManager.LibraryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.Database
{
    class MongoAdapter : IDatabase
    {
        private MongoDB Mongo;


        public Boolean Add(IMovie movie, ISeries series)
        {
            return true;
        }

        public void Update(int id)
        {

        }

        public void Search(String searchItem)
        {

        }

        public Boolean Remove(int id)
        {
            return true;
        }

        public List<Entry> GetAll()
        {
            return null;
        }

        public Entry Get(int id)
        {
            return null;
        }
    }
}
