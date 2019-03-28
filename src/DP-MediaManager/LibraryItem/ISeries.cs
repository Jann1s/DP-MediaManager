using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_MediaManager.LibraryItem
{
    interface ISeries
    {
        Season GetSeason(int index);
        Entry GetEpisode(int index = -1, string name = "");
    }
}
