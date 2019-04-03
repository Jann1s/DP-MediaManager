using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using DP_MediaManager.LibraryItem;
using System.Threading;

namespace DP_MediaManager
{
    class MediaManager : ViewController.ISubject
    {
        private const string CACHEPATH = "cache";

        public int SelectedItem { get; set; }
        public int SelectedSeason { get; set; }
        public int SelectedEpisode { get; set; }

        public static MediaManager Instance { get; set; }

        private List<LibraryFactory> libItems = new List<LibraryFactory>();
        private List<ViewController.IObserver> observers = new List<ViewController.IObserver>();
        private Database.IDatabase database;

        public MediaManager()
        {
            Directory.CreateDirectory(CACHEPATH);
            SelectedEpisode = -1;
            SelectedItem = -1;
            SelectedSeason = -1;

            database = new Database.SqlDB();
            libItems = database.GetAll();

            //Try to download the Poster
            CachePoster();

            ViewController.IObserver mainWindow = new MainWindow();
            Register(mainWindow);

            ((MainWindow)mainWindow).Show();
        }

        public List<LibraryFactory> GetLibrary()
        {
            return libItems;
        }

        public void AddItem(LibraryFactory item)
        {
            database.Add(item);
            libItems.Add(item);
        }

        public void DeleteItem(int id)
        {
            LibraryFactory item = libItems.FindAll(x => x.GetId() == id)[0];
            libItems.RemoveAll(x => x.GetId() == id);
            database.Remove(item);
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

        public void CachePoster()
        {
            foreach (LibraryFactory item in libItems)
            {
                string fileName = GetPosterName(item.GetCardInfo()[0]);

                if (!File.Exists(fileName))
                {
                    DownloadPoster(item.GetCardInfo()[2], fileName);
                }

                if (item is Series)
                {
                    int sCounter = 0;

                    foreach (Season season in ((Series)item).GetSeasons())
                    {
                        string seasonFileName = GetSeasonPosterName(item, sCounter);

                        if (!File.Exists(seasonFileName))
                        {
                            DownloadPoster(season.GetPoster(), seasonFileName);
                        }

                        int eCounter = 0;
                        foreach (Entry entry in season.GetEpisodes())
                        {
                            string episodeFileName = GetEpisodePosterName(item, sCounter, eCounter);

                            if (!File.Exists(episodeFileName))
                            {
                                DownloadPoster(entry.Poster, episodeFileName);
                            }

                            eCounter++;
                        }

                        sCounter++;
                    }
                }
            }
        }

        private void DownloadPoster(string url, string fileLocation)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, fileLocation);
                }
                catch (WebException ex)
                {
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.InternalServerError)
                    {
                        Thread.Sleep(500);
                        DownloadPoster(url, fileLocation);
                    }
                }
            }
        }

        public void Register(ViewController.IObserver observer)
        {
            observers.Add(observer);
        }

        public void Unregister(ViewController.IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            observers.ForEach(p => p.NotifyChange());
        }
    }
}
