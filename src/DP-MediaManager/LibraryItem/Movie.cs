using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    class Movie : IMovie
    {
        private Entry entry;

        public Movie(Entry entry)
        {
            this.entry = entry;
        }

        public Entry GetMovie()
        {
            return entry;
        }
    }
}
