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
        void Add(LibraryItem.LibraryFactory item);
        void Update(LibraryItem.LibraryFactory item);
        List<LibraryItem.LibraryFactory> Search(String searchItem);
        void Remove(LibraryItem.LibraryFactory item);
        List<LibraryItem.LibraryFactory> GetAll();
    }
}
