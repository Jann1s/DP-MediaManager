using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP_MediaManager.LibraryItem;

namespace DP_MediaManager
{
    class MediaManager
    {
        public int SelectedEntry { get; set; }
        public static MediaManager Instance { get; set; }

        private List<LibraryFactory> libItems = new List<LibraryFactory>();
        
        public MediaManager()
        {
            //Movies
            LibraryFactory tmpItem1 = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)tmpItem1).SetEntry(new Entry { Description = "Awesome", Name = "Wall-E" });
            libItems.Add(tmpItem1);

            LibraryFactory tmpItem2 = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)tmpItem2).SetEntry(new Entry { Description = "Aye", Name = "Pirates of the Carebean" });
            libItems.Add(tmpItem2);

            LibraryFactory tmpItem3 = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)tmpItem3).SetEntry(new Entry { Description = "Its time to", Name = "Party" });
            libItems.Add(tmpItem3);

            LibraryFactory tmpItem4 = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)tmpItem4).SetEntry(new Entry { Description = "Enter the", Name = "Matrix" });
            libItems.Add(tmpItem4);

            LibraryFactory tmpItem5 = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)tmpItem5).SetEntry(new Entry { Description = "Wooof", Name = "A Dog" });
            libItems.Add(tmpItem5);

            LibraryFactory tmpItem6 = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)tmpItem6).SetEntry(new Entry { Description = "Or am I", Name = "A Genius" });
            libItems.Add(tmpItem6);

            LibraryFactory tmpItem7 = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)tmpItem7).SetEntry(new Entry { Description = "Quite in", Name = "teresting" });
            libItems.Add(tmpItem7);

            LibraryFactory tmpItem8 = LibraryFactory.GetLibrary(LibraryType.Movie);
            ((Movie)tmpItem8).SetEntry(new Entry { Description = "Lul", Name = "Okaaaay" });
            libItems.Add(tmpItem8);

            //Series
            LibraryFactory tmpItem9 = LibraryFactory.GetLibrary(LibraryType.Series);
            Season tmpSeason = new Season("Awesome show", 3.5f);
            tmpSeason.AddEpisode(new Entry { Description = "wow", Name = "huhu" });
            ((Series)tmpItem9).AddSeason(tmpSeason);
            ((Series)tmpItem9).Name = "Game of Thrones";
            ((Series)tmpItem9).Description = "Just Awesome";
            ((Series)tmpItem9).Poster = "";
            libItems.Add(tmpItem9);
        }

        public List<LibraryFactory> GetLibrary()
        {
            return libItems;
        }
    }
}
