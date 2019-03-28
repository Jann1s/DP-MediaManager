using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    class Series : ISeries
    {
        private List<Season> seasons;

        public Series()
        {
            seasons = new List<Season>();
        }

        public void AddSeason(string description, float rating)
        {
            seasons.Add(new Season(description, rating));

        }

        public Season GetSeason(int index)
        {
            return null;
        }

        public Entry GetEpisode(int index = -1, string name = "")
        {
            return null;
        }
    }
}
