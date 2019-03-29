using DP_MediaManager.LibraryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.Database
{
    interface IDatabase
    {
        Boolean Add(IMovie movie, ISeries series);
        void Update();
        void Search(String searchItem);
        Boolean Remove();
        List<Entry> GetAll();
        Entry Get();
    }
}
