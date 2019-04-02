using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP_MediaManager.LibraryItem;

namespace DP_MediaManager.MediaAPI
{
    class SeriesInformation : IEntryInformation
    {
        private EntryInformation info;
        public SeriesInformation(int id)
        {
            info = new EntryInformation(id);
        }
        List<LibraryFactory> IEntryInformation.GetEntryData()
        {
            List<LibraryFactory> data = new List<LibraryFactory>();
            LibraryFactory series = LibraryFactory.GetLibrary(LibraryType.Series);
            ((Series)series).Name = info.GetTVName();
            ((Series)series).Description = info.GetTVDescription();
            ((Series)series).Poster = info.GetTVPoster();
            foreach (Season s in info.getTVSeasons())
            {
                ((Series)series).AddSeason(s);
            }
            data.Add(series);
            return data;

        }
        public void GetGeneralInformation()
        {

        }
        public List<LibraryItem.Season> GetSeasonList()
        {
            return info.getTVSeasons();
        }
        public Entry GetEpisodeInformation(int seasonNum,int episodeId)
        {
            List<LibraryItem.Season> s = info.getTVSeasons();
            Season season = s[seasonNum-1];

            return season.GetEpisode(episodeId);
        }


    }
}
