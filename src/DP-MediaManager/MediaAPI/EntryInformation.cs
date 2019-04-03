
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
        private int entryId;

        private TMDbClient client;
        private string apiKey;
        private TMDbLib.Objects.Movies.Movie movie;
        private TvShow tvshow;
       
        public EntryInformation(int entryId)
        {
            this.entryId = entryId;

            //Media Api 
            apiKey = "24db2192e9447969b597b0a6823f7e8e";
            client = new TMDbClient(apiKey);
            movie = client.GetMovieAsync(entryId, MovieMethods.Credits).Result;
            tvshow = client.GetTvShowAsync(entryId, TvShowMethods.Credits).Result;
            
        }

        /*
         * 
         * 
         * 
         * 
         * 
         * 
                                  __  __  _____   _____ ___   ___ _  _ _____ _____   __
                                 |  \/  |/ _ \ \ / /_ _| __| | __| \| |_   _| _ \ \ / /
                                 | |\/| | (_) \ V / | || _|  | _|| .` | | | |   /\ V / 
                                 |_|  |_|\___/ \_/ |___|___| |___|_|\_| |_| |_|_\ |_|
         * 
         * 
         * 
         * 
         * 
         * 
         * 
         */

        public double GetMovieRating()
        {
            double rating = movie.VoteAverage;
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
            string img = "http://image.tmdb.org/t/p/w200" + movie.PosterPath;
            return img;
        }

        public List<LibraryItem.Cast> GetMovieCast()
        {
            List<LibraryItem.Cast> castList = new List<LibraryItem.Cast>();

            List<TMDbLib.Objects.Movies.Cast> members = movie.Credits.Cast;

            foreach (TMDbLib.Objects.Movies.Cast member in members)
            {
                LibraryItem.Cast cast = new LibraryItem.Cast();
                string[] fullName = member.Name.Split(' ');
                cast.Firstname = fullName[0];
                cast.Lastname = fullName[1];
                cast.GeneralInformation = "https://www.themoviedb.org/person" + member.ProfilePath; 
                cast.Role = member.Character; 
                castList.Add(cast);
            }
            
            
            return castList;
        }

        public string GetMovieDescription()
        {
            return movie.Overview;
        }



        /*
         * 
         * 
         *   
             ______ _   __  ___ _  _  _____      __  ___ _  _ _____ _____   __
             |_   _\ \ / / / __| || |/ _ \ \    / / | __| \| |_   _| _ \ \ / /
               | |  \ V /  \__ \ __ | (_) \ \/\/ /  | _|| .` | | | |   /\ V / 
               |_|   \_/   |___/_||_|\___/ \_/\_/   |___|_|\_| |_| |_|_\ |_|  
                                                                 
         * 
         * 
         * 
         * 
         * 
         */


        public double GetTVRating()
        {
            double rating = tvshow.VoteAverage;

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

        public string GetTVPoster(string poster = null)
        {
            string img = "";

            if (poster != null)
            {
                img = "http://image.tmdb.org/t/p/w200" + poster;
            }
            else
            {
                img = "http://image.tmdb.org/t/p/w200" + tvshow.PosterPath;
            }
            
            return img;
        }

        public string GetTVDescription()
        {
            return tvshow.Overview;
        }

        public List<LibraryItem.Season> getTVSeasons()
        {
            List<LibraryItem.Season> seasons = new List<Season>();
            int i = 1;
            foreach(TMDbLib.Objects.Search.SearchTvSeason s in tvshow.Seasons)
            {

                TvSeason tvseason = new TvSeason();
                tvseason = client.GetTvSeasonAsync(entryId, i,TvSeasonMethods.Credits).Result;
                
                if (tvseason.Episodes != null)
                {

                    Season tmpSeason = new Season(tvseason.Name, GetTVPoster(tvseason.PosterPath), 0);
                    
                    foreach (TMDbLib.Objects.Search.TvSeasonEpisode e in tvseason.Episodes)
                    {

                        TvEpisode tvEpisode = new TvEpisode();
                        tvEpisode = client.GetTvEpisodeAsync(entryId, i, e.EpisodeNumber,TvEpisodeMethods.Credits).Result;

                        List<LibraryItem.Cast> castList = new List<LibraryItem.Cast>();

                        List<TMDbLib.Objects.TvShows.Cast> members = tvEpisode.Credits.Cast;

                        foreach(TMDbLib.Objects.TvShows.Cast member in members)
                        {
                            LibraryItem.Cast cast = new LibraryItem.Cast();

                            string[] fullName = member.Name.Split(' ');
                            cast.Firstname = fullName[0];
                            cast.Lastname = fullName[1];

                            cast.GeneralInformation = "https://www.themoviedb.org/person" + member.ProfilePath;

                            cast.Role = member.Character;

                            castList.Add(cast);
                        }
                        double rating = tvEpisode.VoteAverage;
                        string name = tvEpisode.Name;
                        DateTime release = tvEpisode.AirDate.Value;
                        string description = tvEpisode.Overview;                        
                        string poster = GetTVPoster(tvEpisode.StillPath);

                        tmpSeason.AddEpisode(new Entry { Description = description, Name = name,Cast = castList, Release = release, Rating = rating, Poster = poster });
                    }
                    seasons.Add(tmpSeason);
                }

                i++;
            }
            
            return seasons;
        }
    }
}
