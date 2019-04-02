using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    public class Season
    {
        private string description;
        private float rating;
        private string poster;
        private List<Entry> episodes;

        public Season(string description, string poster, float rating = -1f)
        {
            episodes = new List<Entry>();

            this.description = description;
            this.rating = rating;
            this.poster = poster;
        }

        public void AddEpisode(Entry episode)
        {
            episodes.Add(episode);
        }

        public Entry GetEpisode(int index)
        {
            return episodes[index];
        }

        public List<Entry> GetEpisodes()
        {
            return episodes;
        }

        public string GetDescription()
        {
            return description;
        }

        public float GetRating()
        {
            return rating;
        }

        public string GetPoster()
        {
            return poster;
        }
    }
}
