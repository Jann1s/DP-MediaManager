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

        private List<IMovie> movies = new List<IMovie>();
        
        public MediaManager()
        {
            movies.Add(new Movie(new Entry { Description = "Awesome", Name = "Wall-E" }));
            movies.Add(new Movie(new Entry { Description = "Aye", Name = "Pirates of the Carebean" }));
            movies.Add(new Movie(new Entry { Description = "Its time to", Name = "Party" }));
            movies.Add(new Movie(new Entry { Description = "Enter the", Name = "Matrix" }));
            movies.Add(new Movie(new Entry { Description = "Awesome", Name = "Wall-E" }));
            movies.Add(new Movie(new Entry { Description = "Wooof", Name = "A Dog" }));
            movies.Add(new Movie(new Entry { Description = "Quite in", Name = "teresting" }));
            movies.Add(new Movie(new Entry { Description = "Or am I", Name = "A Genius" }));
        }

        public List<IMovie> GetLibrary()
        {
            return movies;
        }
    }
}
