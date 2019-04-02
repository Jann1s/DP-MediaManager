using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    public abstract class LibraryFactory
    {
        public static LibraryFactory GetLibrary(LibraryType libraryType, int id)
        {
            switch (libraryType)
            {
                case LibraryType.Movie:
                    return new Movie(id);
                case LibraryType.Series:
                    return new Series(id);
                default:
                    return null;
            }
        }

        public abstract List<string> GetCardInfo();

        public abstract Entry GetDetails(int season = -1, int episode = -1);

        public abstract int GetId();
    }

    public enum LibraryType {
        Movie,
        Series
    }
}
