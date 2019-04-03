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
        private string LoadConnectionString(String id = "Default")
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
                else if (item is Series)
                {
                    SQLiteCommand commandSeries = new SQLiteCommand("INSERT INTO Series (series_id, name, description, genre, poster) VALUES (@seriesid, @name, @description, @genre, @poster)", cnn);
                    commandSeries.Parameters.AddWithValue("@seriesid", ((Series)item).GetId());
                    commandSeries.Parameters.AddWithValue("@name", ((Series)item).Name);
                    commandSeries.Parameters.AddWithValue("@description", ((Series)item).Description);
                    commandSeries.Parameters.AddWithValue("@genre", ((Series)item).GetGenre());
                    commandSeries.Parameters.AddWithValue("@poster", ((Series)item).Poster);

                    commandSeries.ExecuteNonQuery();

                    foreach (Season season in ((Series)item).GetSeasons())
                    {
                        SQLiteCommand commandSeason = new SQLiteCommand("INSERT INTO Season (series_id, description, poster) VALUES (@series, @description, @poster)", cnn);
                        commandSeason.Parameters.AddWithValue("@series", ((Series)item).GetId());
                        commandSeason.Parameters.AddWithValue("@description", season.GetDescription());
                        commandSeason.Parameters.AddWithValue("@poster", season.GetPoster());

                        commandSeason.ExecuteNonQuery();

                        //Entry Tabelle -> ID bekommen vom letzten insert
                        SQLiteCommand commandSeasonID = new SQLiteCommand("SELECT * FROM Season WHERE series_id = @series AND description = @desc", cnn);
                        commandSeasonID.Parameters.AddWithValue("@series", ((Series)item).GetId());
                        commandSeasonID.Parameters.AddWithValue("@desc", season.GetDescription());

                        long seasonId = (long)commandSeasonID.ExecuteScalar();

                        foreach (Entry episode in season.GetEpisodes())
                        {
                            SQLiteCommand commandEntry = new SQLiteCommand("INSERT INTO Entry (name, description, releaseYear, poster) VALUES (@name, @desc, @release, @poster)", cnn);
                            commandEntry.Parameters.AddWithValue("@name", episode.Name);
                            commandEntry.Parameters.AddWithValue("@desc", episode.Description);
                            commandEntry.Parameters.AddWithValue("@release", episode.Release);
                            commandEntry.Parameters.AddWithValue("@poster", episode.Poster);

                            commandEntry.ExecuteNonQuery();

                            //Entry Tabelle -> ID bekommen vom letzten insert
                            SQLiteCommand commandID = new SQLiteCommand("SELECT * FROM Entry WHERE name = @name AND description = @desc", cnn);
                            commandID.Parameters.AddWithValue("@name", episode.Name);
                            commandID.Parameters.AddWithValue("@desc", episode.Description);

                            long entryId = (long)commandID.ExecuteScalar();

                            SQLiteCommand commandSeasonEntry = new SQLiteCommand("INSERT INTO Entry_Season (entry_id, season_id) VALUES (@entry, @season)", cnn);
                            commandSeasonEntry.Parameters.AddWithValue("@entry", entryId);
                            commandSeasonEntry.Parameters.AddWithValue("@season", seasonId);

                            commandSeasonEntry.ExecuteNonQuery();
                        }
                    }
                }

                cnn.Close();
            }
        }

        public void Update(LibraryItem.LibraryFactory item)
        {

        }

        public List<LibraryFactory> Search(String searchItem)
        {
            return null;
        }

        public void Remove(LibraryFactory item)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                //If Movie
                if (item is Movie)
                {
                    //Delete From Entry Table
                    Entry movie = ((Movie)item).GetMovie();

                    SQLiteCommand commandEntry = new SQLiteCommand("DELETE FROM Entry WHERE name = @name AND description = @desc", cnn);
                    commandEntry.Parameters.AddWithValue("@name", movie.Name);
                    commandEntry.Parameters.AddWithValue("@desc", movie.Description);

                    commandEntry.ExecuteNonQuery();

                    //Delete from Movie Table
                    SQLiteCommand commandMovie = new SQLiteCommand("DELETE FROM Movie WHERE movie_id = @movieId", cnn);
                    commandMovie.Parameters.AddWithValue("@movieId", ((Movie)item).GetId());

                    commandMovie.ExecuteNonQuery();
                }
                //If Series
                else if (item is Series)
                {
                    //Get Series id
                    int seriesId = item.GetId();

                    //Get Season id
                    SQLiteCommand commandSeason = new SQLiteCommand("SELECT * FROM Season WHERE series_id = @seriesid", cnn);
                    commandSeason.Parameters.AddWithValue("@seriesid", seriesId);
                    SQLiteDataReader readerSeason = commandSeason.ExecuteReader();

                    while (readerSeason.Read())
                    {
                        int seasonId = readerSeason.GetInt32(0);

                        //Get Entry Id
                        SQLiteCommand commandEntrySeason = new SQLiteCommand("SELECT * FROM Entry_Season WHERE season_id = @seasonid", cnn);
                        commandEntrySeason.Parameters.AddWithValue("@seasonid", seasonId);
                        SQLiteDataReader readerEntry = commandEntrySeason.ExecuteReader();

                        while (readerEntry.Read())
                        {
                            int entryId = readerEntry.GetInt32(0);
                            //Delete Episodes
                            SQLiteCommand commandEntryDelete = new SQLiteCommand("DELETE FROM Entry WHERE entry_id = @entryid", cnn);
                            commandEntryDelete.Parameters.AddWithValue("@entryid", entryId);
                            commandEntryDelete.ExecuteNonQuery();

                            //Delete Entry_Season
                            SQLiteCommand commandEntrySeasonDelete = new SQLiteCommand("DELETE FROM Entry_Season WHERE entry_id = @entryid", cnn);
                            commandEntrySeasonDelete.Parameters.AddWithValue("@entryid", entryId);
                            commandEntrySeasonDelete.ExecuteNonQuery();
                        }
                    }

                    //Delete Seasons
                    SQLiteCommand commandSeasonDelete = new SQLiteCommand("DELETE FROM Season WHERE series_id = @series", cnn);
                    commandSeasonDelete.Parameters.AddWithValue("@series", seriesId);
                    commandSeasonDelete.ExecuteNonQuery();

                    //Delete Series
                    SQLiteCommand commandSeriesDelete = new SQLiteCommand("DELETE FROM Series WHERE series_id = @seriesid", cnn);
                    commandSeriesDelete.Parameters.AddWithValue("@seriesid", seriesId);
                    commandSeriesDelete.ExecuteNonQuery();
                }

                cnn.Close();
            }
        }

        public List<LibraryFactory> GetAll()
        {
            List<LibraryFactory> lib = new List<LibraryFactory>();
            using (SQLiteConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();

                SQLiteCommand commandMovie = new SQLiteCommand("SELECT * FROM Movie", cnn);
                SQLiteDataReader readerMovie = commandMovie.ExecuteReader();

                while (readerMovie.Read())
                {
                    LibraryFactory movie = LibraryFactory.GetLibrary(LibraryType.Movie, readerMovie.GetInt32(2), readerMovie.GetString(1));
                    SQLiteCommand commandEntry = new SQLiteCommand("SELECT * FROM Entry WHERE entry_id = @entryid", cnn);
                    commandEntry.Parameters.AddWithValue("@entryid", readerMovie.GetInt32(0));
                    SQLiteDataReader readerEntry = commandEntry.ExecuteReader();

                    while (readerEntry.Read())
                    {
                        Entry entry = new Entry()
                        {
                            Name = readerEntry.GetString(1),
                            Description = readerEntry.GetString(2),
                            Release = readerEntry.GetDateTime(3),
                            Poster = readerEntry.GetString(4)
                        };

                        ((Movie)movie).SetEntry(entry);
                    }

                    lib.Add(movie);
                }

                SQLiteCommand commandSeries = new SQLiteCommand("SELECT * FROM Series", cnn);
                SQLiteDataReader readerSeries = commandSeries.ExecuteReader();

                while (readerSeries.Read())
                {
                    LibraryFactory series = LibraryFactory.GetLibrary(LibraryType.Series, readerSeries.GetInt32(0), readerSeries.GetString(3));
                    ((Series)series).Description = readerSeries.GetString(2);
                    ((Series)series).Poster = readerSeries.GetString(4);
                    ((Series)series).Name = readerSeries.GetString(1);

                    SQLiteCommand commandSeason = new SQLiteCommand("SELECT * FROM Season WHERE series_id = @seriesid ORDER BY season_id ASC", cnn);
                    commandSeason.Parameters.AddWithValue("@seriesid", readerSeries.GetInt32(0));
                    SQLiteDataReader readerSeason = commandSeason.ExecuteReader();

                    while (readerSeason.Read())
                    {
                        Season season = new Season(readerSeason.GetString(2), readerSeason.GetString(3));
                        
                        SQLiteCommand commandEntrySeason = new SQLiteCommand("SELECT * FROM Entry_Season WHERE season_id = @seasonid ORDER BY 'index' ASC", cnn);
                        commandEntrySeason.Parameters.AddWithValue("@seasonid", readerSeason.GetInt32(0));
                        SQLiteDataReader readerEntrySeason = commandEntrySeason.ExecuteReader();

                        while (readerEntrySeason.Read())
                        {
                            SQLiteCommand commandEntry = new SQLiteCommand("SELECT * FROM Entry WHERE entry_id = @entryid", cnn);
                            commandEntry.Parameters.AddWithValue("@entryid", readerEntrySeason.GetInt32(0));
                            SQLiteDataReader readerEntry = commandEntry.ExecuteReader();

                            while (readerEntry.Read())
                            {
                                Entry entry = new Entry()
                                {
                                    Name = readerEntry.GetString(1),
                                    Description = readerEntry.GetString(2),
                                    Release = readerEntry.GetDateTime(3),
                                    Poster = readerEntry.GetString(4)
                                };

                                season.AddEpisode(entry);
                            }
                        }

                        ((Series)series).AddSeason(season);
                    }

                    lib.Add(series);
                }
            }

            return lib;
        }
    }
}
