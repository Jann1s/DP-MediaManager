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
        private List<IMovie> movies;

        public MediaManager()
        {
            movies = new List<IMovie>();

            movies.Add(new Movie(new Entry { Description = "Awesome", Name = "Wall-E" }));
            movies.Add(new Movie(new Entry { Description = "Aye", Name = "Pirates of the Carebean" }));
            movies.Add(new Movie(new Entry { Description = "Its time to", Name = "Party" }));
            movies.Add(new Movie(new Entry { Description = "Enter the", Name = "Matrix" }));
            movies.Add(new Movie(new Entry { Description = "Awesome", Name = "Wall-E" }));
        }
    }
}
