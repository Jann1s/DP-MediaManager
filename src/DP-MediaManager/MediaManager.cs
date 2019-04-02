using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using DP_MediaManager.LibraryItem;

namespace DP_MediaManager
{
    class MediaManager
    {
        private const string CACHEPATH = "cache";

        public int SelectedItem { get; set; }
        public int SelectedSeason { get; set; }
        public int SelectedEpisode { get; set; }

        public static MediaManager Instance { get; set; }

        private List<LibraryFactory> libItems = new List<LibraryFactory>();
        
        public MediaManager()
        {
            System.IO.Directory.CreateDirectory(CACHEPATH);
            SelectedEpisode = -1;
            SelectedItem = -1;
            SelectedSeason = -1;

            //Movies
            LibraryFactory tmpItem1 = LibraryFactory.GetLibrary(LibraryType.Movie, 1);
            ((Movie)tmpItem1).SetEntry(new Entry { Description = "Awesome", Name = "Wall-E", Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/7DC3ggzkSTCYUk2FvPEvvhSjNbG.jpg" });
            libItems.Add(tmpItem1);

            LibraryFactory tmpItem2 = LibraryFactory.GetLibrary(LibraryType.Movie, 2);
            ((Movie)tmpItem2).SetEntry(new Entry { Description = "Aye", Name = "Pirates of the Carebean", Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/7dKv4V9T8larDhU2frMQZlvffCV.jpg" });
            libItems.Add(tmpItem2);

            LibraryFactory tmpItem3 = LibraryFactory.GetLibrary(LibraryType.Movie, 3);
            ((Movie)tmpItem3).SetEntry(new Entry { Description = "Its time to", Name = "Party", Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/k0Ro4c88NilTjDvxyRG6X9VuAAP.jpg" });
            libItems.Add(tmpItem3);

            LibraryFactory tmpItem4 = LibraryFactory.GetLibrary(LibraryType.Movie, 4);
            ((Movie)tmpItem4).SetEntry(new Entry { Description = "Enter the", Name = "Matrix", Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/bfuMEU1Sc4K24j2lSIvDQDCD6ix.jpg" });
            libItems.Add(tmpItem4);

            LibraryFactory tmpItem5 = LibraryFactory.GetLibrary(LibraryType.Movie, 5);
            ((Movie)tmpItem5).SetEntry(new Entry { Description = "Wooof", Name = "A Dog", Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/rYQmzdVkyiGIAe3r37jTJXmwk9A.jpg" });
            libItems.Add(tmpItem5);

            LibraryFactory tmpItem6 = LibraryFactory.GetLibrary(LibraryType.Movie, 6);
            ((Movie)tmpItem6).SetEntry(new Entry { Description = "Or am I", Name = "A Genius", Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/t9pirEsDxe102mSQGoDZWBbdcCD.jpg" });
            libItems.Add(tmpItem6);

            LibraryFactory tmpItem7 = LibraryFactory.GetLibrary(LibraryType.Movie, 7);
            ((Movie)tmpItem7).SetEntry(new Entry { Description = "Quite in", Name = "teresting", Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/kmFzZxYyTUXmNIADubceGj41OxV.jpg" });
            libItems.Add(tmpItem7);

            LibraryFactory tmpItem8 = LibraryFactory.GetLibrary(LibraryType.Movie, 8);
            ((Movie)tmpItem8).SetEntry(new Entry { Description = "Lul", Name = "Okaaaay", Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/brHFow2DdcsyqalMqk43irnUoQl.jpg" });
            libItems.Add(tmpItem8);

            //Series
            LibraryFactory tmpItem9 = LibraryFactory.GetLibrary(LibraryType.Series, 1399);
            Season tmpSeason = new Season("Awesome show", "https://image.tmdb.org/t/p/original/zwaj4egrhnXOBIit1tyb4Sbt3KP.jpg", 3.5f);
            tmpSeason.AddEpisode(new Entry { Description = "wow", Name = "huhu", Poster = "https://image.tmdb.org/t/p/original/wrGWeW4WKxnaeA8sxJb2T9O6ryo.jpg" });

            ((Series)tmpItem9).AddSeason(tmpSeason);
            ((Series)tmpItem9).Name = "Game of Thrones";
            ((Series)tmpItem9).Description = "Just Awesome";
            ((Series)tmpItem9).Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/op8kIseUiSCVTT05Z067GzkDB28.jpg";
            libItems.Add(tmpItem9);


            //Try to download the Poster
            CachePoster();
        }

        public List<LibraryFactory> GetLibrary()
        {
            return libItems;
        }

        public void DeleteItem(int id)
        {
            libItems.RemoveAll(x => x.GetId() == id);
        }

        public string GetPosterName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }

            fileName = fileName.Replace(" ", "__");
            fileName = Path.Combine("cache", fileName);

            return fileName + ".jpg";
        }

        public string GetSeasonPosterName(LibraryFactory item, int index)
        {
            string file = item.GetCardInfo()[0] + "-_-" + "S" + index.ToString();
            return GetPosterName(file);
        }

        public string GetEpisodePosterName(LibraryFactory item, int season, int episode)
        {
            string file = item.GetCardInfo()[0] + "-_-S" + season.ToString() + "-_-E" + episode.ToString();
            return GetPosterName(file);
        }

        private void CachePoster()
        {
            foreach (LibraryFactory item in libItems)
            {
                string fileName = GetPosterName(item.GetCardInfo()[0]);

                if (!File.Exists(fileName))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(item.GetCardInfo()[2], fileName);
                    }
                }

                if (item is Series)
                {
                    int sCounter = 0;

                    foreach (Season season in ((Series)item).GetSeasons())
                    {
                        string seasonFileName = GetSeasonPosterName(item, sCounter);

                        if (!File.Exists(seasonFileName))
                        {
                            using (WebClient client = new WebClient())
                            {
                                client.DownloadFile(season.GetPoster(), seasonFileName);
                            }
                        }

                        int eCounter = 0;
                        foreach (Entry entry in season.GetEpisodes())
                        {
                            string episodeFileName = GetEpisodePosterName(item, sCounter, eCounter);

                            if (!File.Exists(episodeFileName))
                            {
                                using (WebClient client = new WebClient())
                                {
                                    client.DownloadFile(entry.Poster, episodeFileName);
                                }
                            }

                            eCounter++;
                        }

                        sCounter++;
                    }
                }
            }
        }
    }
}
