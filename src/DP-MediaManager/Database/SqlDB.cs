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
            string fixedConnectionString = ConfigurationManager.ConnectionStrings[id].ConnectionString.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory);

            return fixedConnectionString;
        }

        public void Add(LibraryFactory item)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                //If Movie
                if (item is Movie)
                {
                    //Entry Tabelle inserten
                    Entry movie = ((Movie)item).GetMovie();
                    
                    SQLiteCommand commandEntry = new SQLiteCommand("INSERT INTO Entry (name, description, releaseYear, poster) VALUES (@name, @desc, @release, @poster)", cnn);
                    commandEntry.Parameters.AddWithValue("@name", movie.Name);
                    commandEntry.Parameters.AddWithValue("@desc", movie.Description);
                    commandEntry.Parameters.AddWithValue("@release", movie.Release);
                    commandEntry.Parameters.AddWithValue("@poster", movie.Poster);

                    commandEntry.ExecuteNonQuery();

                    //Entry Tabelle -> ID bekommen vom letzten insert
                    SQLiteCommand commandID= new SQLiteCommand("SELECT * FROM Entry WHERE name = @name AND description = @desc", cnn);
                    commandID.Parameters.AddWithValue("@name", movie.Name);
                    commandID.Parameters.AddWithValue("@desc", movie.Description);

                    long entryId = (long)commandID.ExecuteScalar();

                    //Movie Tabelle -> movie_id und entry_id inserten
                    SQLiteCommand commandMovie = new SQLiteCommand("INSERT INTO Movie (movie_id, entry_id, genre) VALUES (@movieId, @entryId, @genre)", cnn);
                    commandMovie.Parameters.AddWithValue("@movieId", ((Movie)item).GetId());
                    commandMovie.Parameters.AddWithValue("@entryId", entryId);
                    commandMovie.Parameters.AddWithValue("@genre", ((Movie)item).GetGenre());

                    commandMovie.ExecuteNonQuery();
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

                cnn.Close();
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
