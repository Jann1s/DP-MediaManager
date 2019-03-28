using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    class Season
    {
        private string description;
        private float rating;
        private List<Entry> episodes;

        public Season(string description, float rating)
        {
            episodes = new List<Entry>();

            this.description = description;
            this.rating = rating;
        }

        public void AddEpisode(Entry episode)
        {
            episodes.Add(episode);
        }

        public Entry GetEpisode(int index)
        {
            return episodes[index];
        }

        public string GetDescription()
        {
            return description;
        }

        public float GetRating()
        {
            return rating;
        }
    }
}
