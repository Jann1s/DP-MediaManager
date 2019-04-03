using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    class Movie : LibraryFactory
    {
        private Entry entry;
        private int id;
        private string genre;

        public Movie(int id, string genre)
        {
            this.id = id;
            this.genre = genre;
        }

        public void SetEntry(Entry entry)
        {
            this.entry = entry;
        }

        public Entry GetMovie()
        {
            return entry;
        }

        public override List<string> GetCardInfo()
        {
            List<string> card = new List<string>();
            card.Add(entry.Name);
            card.Add(entry.Description);
            card.Add(entry.Poster);

            return card;
        }

        public override Entry GetDetails(int season = -1, int episode = -1)
        {
            return entry;
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
