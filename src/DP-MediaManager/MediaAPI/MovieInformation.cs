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
        private int id;

        public MovieInformation(int id)
        {
            info = new EntryInformation(id);
            this.id = id;
        }

        public void GetGeneralInformation()
        {

        }

        public LibraryFactory GetEntryData()
        {
            LibraryFactory movie = LibraryFactory.GetLibrary(LibraryType.Movie, id);
            ((Movie)movie).SetEntry(new Entry {
                Description = info.GetMovieDescription(),
                Name = info.GetMovieName(),
                Release = info.GetMovieReleaseDate(),
                Poster = info.GetMoviePoster(),
                Cast = info.GetMovieCast() });

            return movie;
        }
    }
}
