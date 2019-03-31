
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
        public string GetRating()
        {
            //Should work but doesnt
            string review = null;
            foreach (Review r in movie.Reviews.Results)
            {
                review += "Author:" + r.Author;
                review += "\t";
                review += "Review:" + r.Content;
                review += "\t";
            }
            return review;
        }
        public DateTime GetReleaseDate()
        {
            return movie.ReleaseDate.Value;
        }
        public string GetName()
        {
            return movie.Title;
        }
        public string GetGenre()
        {
            return null;
        }
        public string GetPoster()
        {
            //Same shit as rating
            string img = null;
            foreach(ImageData poster in movie.Images.Posters)
            {
                img += poster.FilePath.ToString();
            }
            return img;
        }
        
        public List<LibraryItem.Cast> GetCast()
        {
            List<LibraryItem.Cast> cast = new List<LibraryItem.Cast>();
            foreach( Cast member in movie.Credits.Cast)
            {
                //Cast constructor needs name role 
                //  member.Name;
                //  member.Character;
                
                cast.Add( new LibraryItem.Cast());
              
               
                
            }
            return cast;
        }
    }
}
