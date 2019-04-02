using Dapper;
using DP_MediaManager.LibraryItem;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.Database
{
    class SqlDB : IDatabase
    {
        public string LoadConnectionString(String id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public void Add(LibraryItem.LibraryFactory item)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                //If Movie
                if (item is LibraryItem.Movie)
                {
                    LibraryItem.Entry movie = ((LibraryItem.Movie)item).GetMovie();
                    cnn.Execute("INSERT INTO Entry (name, description, releaseYear, poster) values (@Name, @Description, @Release, @Poster)", movie);
                }

                //If Series
                else if (item is LibraryItem.Series)
                {
                    String seriesName = ((LibraryItem.Series)item).Name;
                    String seriesDescription = ((LibraryItem.Series)item).Description;
                    String seriesPoster = ((LibraryItem.Series)item).Poster;
                    List<Season> seasons = ((LibraryItem.Series)item).GetSeasons();

                    //Series - Data
                    cnn.Execute("INSERT INTO Entry (name, description, poster) values (" + seriesName + ", " + seriesDescription + ", " + seriesPoster + ")");

                    //Season - Data
                    foreach (var season in seasons)
                    {
                        string seasonDescription = season.GetDescription();
                        cnn.Execute("INSERT INTO Season (name, description, poster) values (" + seasonDescription + ")");
                        List<Entry> episodes = season.GetEpisodes();

                        //Episodes - Data
                        foreach (var episode in episodes)
                        {
                            String episodeName = episode.Name;
                            String episodeDescription = episode.Description;
                            String episodePoster = episode.Poster;
                            DateTime episodeRelease = episode.Release;

                            cnn.Execute("INSERT INTO Entry (name, description, poster) values (" + episodeName + episodeDescription + episodeDescription + episodePoster + ")");
                        }
                    }

                }
            }
        }

        public void Update(LibraryItem.LibraryFactory item)
        {

        }

        public List<LibraryItem.LibraryFactory> Search(String searchItem)
        {
            return null;
        }

        public void Remove(LibraryItem.LibraryFactory item)
        {

        }

        public List<LibraryItem.LibraryFactory> GetAll()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Entry>("SELECT * FROM entry", new DynamicParameters());
                //return output.ToList();
                return null;
            }
        }
    }
}
