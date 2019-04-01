using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    abstract class LibraryFactory
    {
        public static LibraryFactory getLibrary(LibraryType libraryType)
        {
            switch (libraryType)
            {
                case LibraryType.Movie:
                    return new Movie();
                case LibraryType.Series:
                    return new Series();
                default:
                    return null;
            }
        }

        public abstract List<string> GetCardInfo();

        public abstract Entry GetDetails(int season = -1, int episode = -1);
    }

    enum LibraryType {
        Movie,
        Series
    }
}
