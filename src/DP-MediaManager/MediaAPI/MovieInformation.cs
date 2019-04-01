using DP_MediaManager.LibraryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.MediaAPI
{
    class MovieInformation : IEntryInformation
    {
        private EntryInformation info;
        public MovieInformation(int id)
        {
            info = new EntryInformation(id);
        }
        public void GetGeneralInformation()
        {

        }
        List<LibraryFactory> IEntryInformation.GetEntryData()
        {
            List<LibraryFactory> data = new List<LibraryFactory>();
            LibraryFactory movie = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)movie).SetEntry(new Entry { Description = info.GetDescription(), Name = info.GetName() });
            data.Add(movie);
            return data;
        }
    }
}
