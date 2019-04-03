using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    public class Entry
    {
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public string FileLocation { get; set; }
        public string Description { get; set; }
        public List<Cast> Cast { get; set; }
        public string Poster { get; set; }
        public double Rating { get; set; }
    }
}
