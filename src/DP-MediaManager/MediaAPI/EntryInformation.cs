
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;

namespace DP_MediaManager.MediaAPI
{
    class EntryInformation
    {
        private TMDbClient client;
        private string apiKey;
        private int entryId;
        private Movie movie;

        public EntryInformation(int entryId)
        {
            this.entryId = entryId;
            apiKey = "24db2192e9447969b597b0a6823f7e8e";
            client = new TMDbClient(apiKey);
            movie = client.GetMovieAsync(entryId).Result;
    
            
        }
        public List<String> GetRating()
        {
            List<String> reviews = new List<string>();
            foreach (Review r in movie.Reviews.Results)
            {
                reviews.Add(r.Content);
            }
            return reviews;
        }
        public DateTime GetReleaseDate()
        {
            return movie.ReleaseDate.Value;
        }
        public string GetName()
        {
            return movie.Title;
        }
        public List<String> GetGenre()
        {
            List<String> s = new List<string>();
            foreach (Genre genre in movie.Genres)
            {
                s.Add(genre.Name);
            }
            return s;
        }
        public string GetPoster()
        {
            //Same shit as rating
            string img = null;
            foreach(ImageData poster in movie.Images.Posters)
            {
                //
            }
            return img;
        }
        public List<LibraryItem.Cast> GetCast()
        {
            List<LibraryItem.Cast> castList = new List<LibraryItem.Cast>();
            foreach( Cast member in movie.Credits.Cast)
            {
                LibraryItem.Cast cast = new LibraryItem.Cast();
                cast.Firstname = member.Name;
                //cast.Lastname = ;
                //cast.GeneralInformation = ;
                cast.Role = member.Character;
                castList.Add(cast);
            }
            return castList;
        }
    }
}
