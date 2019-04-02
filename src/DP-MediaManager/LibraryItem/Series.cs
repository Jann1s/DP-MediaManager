using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    class Series : LibraryFactory, ISeries
    {
        private List<Season> seasons;
        private int id;
        private string genre;

        public string Description { get; set; }
        public string Name { get; set; }
        public string Poster { get; set; }

        public Series(int id, string genre)
        {
            seasons = new List<Season>();
            this.id = id;
            this.genre = genre;
        }

        public void AddSeason(string description, string poster, float rating = -1f)
        {
            seasons.Add(new Season(description, poster, rating));

        }

        public void AddSeason(Season season)
        {
            seasons.Add(season);

        }

        public Season GetSeason(int index)
        {
            return seasons[index];
        }

        public List<Season> GetSeasons()
        {
            return seasons;
        }

        public override Entry GetDetails(int season = -1, int episode = -1)
        {
            return seasons[season].GetEpisode(episode);
        }

        public override List<string> GetCardInfo()
        {
            List<string> card = new List<string>();
            card.Add(Name);
            card.Add(Description);
            card.Add(Poster);

            return card;
        }

        public override int GetId()
        {
            return id;
        }

        public override string GetGenre()
        {
            return genre;
        }
    }
}
