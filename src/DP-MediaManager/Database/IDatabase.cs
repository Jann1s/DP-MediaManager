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
        void Add(LibraryFactory item);
        void Update(LibraryFactory item);
        List<LibraryFactory> Search(String searchItem);
        void Remove(LibraryFactory item);
        List<LibraryFactory> GetAll();
    }
}
