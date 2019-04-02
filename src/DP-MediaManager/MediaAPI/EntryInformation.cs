
using DP_MediaManager.LibraryItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Reviews;
using TMDbLib.Objects.TvShows;

namespace DP_MediaManager.MediaAPI
{
    class EntryInformation
    {
        private TMDbClient client;
        private string apiKey;
        private int entryId;
        private TMDbLib.Objects.Movies.Movie movie;
        private TvShow tvshow;
       

        public EntryInformation(int entryId)
        {
            this.entryId = entryId;
            apiKey = "24db2192e9447969b597b0a6823f7e8e";
            client = new TMDbClient(apiKey);
            movie = client.GetMovieAsync(entryId).Result;
            tvshow = client.GetTvShowAsync(entryId).Result;
            
        }
        //MOVIE STUFF
        public double GetMovieRating()
        {
            double rating = movie.VoteAverage;
            //List<String> reviews = new List<string>();
            //foreach (Review r in movie.Reviews.Results)
            //{
            //    reviews.Add(r.Content);
            //}
            return rating;
        }
        public DateTime GetMovieReleaseDate()
        {
            return movie.ReleaseDate.Value;
        }
        public string GetMovieName()
        {
            return movie.Title;
        }
        public string GetMovieGenre()
        {
            string s = null;
            foreach (Genre genre in movie.Genres)
            {
                s += genre.Name;
                s += ",";
            }
            return s;
        }
        public string GetMoviePoster()
        {
            //Same shit as rating


            string img = movie.PosterPath;

            // poster.FilePath;

            return img;
        }
        public List<LibraryItem.Cast> GetMovieCast()
        {
            List<LibraryItem.Cast> castList = new List<LibraryItem.Cast>();
            foreach (TMDbLib.Objects.Movies.Cast member in movie.Credits.Cast)
            {
                LibraryItem.Cast cast = new LibraryItem.Cast();
                cast.Firstname = member.Name;
                //cast.Lastname = ;
                cast.GeneralInformation = member.ProfilePath; ;
                cast.Role = member.Character;

                castList.Add(cast);
            }
            return castList;
        }
        //public List<LibraryItem.Cast> GetDirectors()
        //{
        //    List<LibraryItem.Cast> directorList = new List<LibraryItem.Cast>();
        //    foreach (Cast director in movie.Credits)
        //    {

        //    }
        //}
        public string GetMovieDescription()
        {
            return movie.Tagline;
        }

        //TV SHOW STUFF
        public double GetTVRating()
        {
            double rating = tvshow.VoteAverage;
            //List<String> reviews = new List<string>();
            //foreach (Review r in movie.Reviews.Results)
            //{
            //    reviews.Add(r.Content);
            //}
            return rating;
        }
        public DateTime GetTVReleaseDate()
        {
            return tvshow.FirstAirDate.Value;
        }
        public string GetTVName()
        {
            return tvshow.Name;
        }
        public string GetTVGenre()
        {
            string s = null;
            foreach (Genre genre in tvshow.Genres)
            {
                s += genre.Name;
                s += ",";
            }
            return s;
        }
        public string GetTVPoster()
        {
            //Same shit as rating


            string img = tvshow.PosterPath;

            // poster.FilePath;

            return img;
        }
        public List<LibraryItem.Cast> GetTVCast()
        {
            List<LibraryItem.Cast> castList = new List<LibraryItem.Cast>();
            foreach (TMDbLib.Objects.TvShows.Cast member in tvshow.Credits.Cast)
            {
                LibraryItem.Cast cast = new LibraryItem.Cast();
                cast.Firstname = member.Name;
                //cast.Lastname = ;
                cast.GeneralInformation = member.ProfilePath; 
                cast.Role = member.Character;

                castList.Add(cast);
            }
            return castList;
        }
        public List<LibraryItem.Cast> GetTVDirectors()
        {
            List<LibraryItem.Cast> directorList = new List<LibraryItem.Cast>();
            //string director = null; 
            foreach(TMDbLib.Objects.TvShows.CreatedBy d in tvshow.CreatedBy)
            {
                LibraryItem.Cast director = new LibraryItem.Cast();
                director.Firstname = d.Name;
                director.GeneralInformation = d.ProfilePath; 
                directorList.Add(director);

                //director += d.Name;
               // director += ",";
            }
            return directorList;
        }
        public string GetTVDescription()
        {
            return tvshow.OriginalName;
        }
        public List<LibraryItem.Season> getTVSeasons()
        {
            List<LibraryItem.Season> seasons = new List<Season>();
            int i = 1;
            //while(i <= tvshow.NumberOfSeasons)
            foreach(TMDbLib.Objects.Search.SearchTvSeason s in tvshow.Seasons)
            {
                TvSeason tvseason = new TvSeason();
                tvseason = client.GetTvSeasonAsync(entryId, i).Result;
                
                Season tmpSeason = new Season(tvseason.Name, 0);
                foreach (TMDbLib.Objects.Search.TvSeasonEpisode e in tvseason.Episodes)
                {
                    TvEpisode tvEpisode = new TvEpisode();
                    tvEpisode = client.GetTvEpisodeAsync(entryId, i, e.EpisodeNumber).Result;

                    string name = tvEpisode.Name;
                    DateTime release = tvEpisode.AirDate.Value;
                    string description = tvEpisode.Overview;
                    List<LibraryItem.Cast> cast = GetEpisodeCast(tvEpisode);
                    string poster = null;//tvEpisode.Images.Stills
                    tmpSeason.AddEpisode(new Entry { Description = description, Name = name, Release = release, Cast = cast, Poster = poster});
                }
                seasons.Add(tmpSeason);


                i++;

            }


            return seasons;
           




        }
        public List<LibraryItem.Cast> GetEpisodeCast(TvEpisode e)
        {
            List<LibraryItem.Cast> castList = new List<LibraryItem.Cast>();
            
            foreach (TMDbLib.Objects.TvShows.Cast d in e.Credits.Cast)
            {
                LibraryItem.Cast cast = new LibraryItem.Cast();
                cast.Firstname = d.Name;
                cast.GeneralInformation = d.ProfilePath;
                cast.Role = d.Character;

                castList.Add(cast);
            }
            return castList;
        }
    }
}
