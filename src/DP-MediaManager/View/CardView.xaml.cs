using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DP_MediaManager.View
{
    /// <summary>
    /// Interaction logic for CardView.xaml
    /// </summary>
    public partial class CardView : UserControl
    {
        public CardView(LibraryItem.LibraryFactory item, int season = -1, int episode = -1)
        {
            InitializeComponent();

            string title = "";
            string poster = "";

            if (item is LibraryItem.Movie)
            {
                title = item.GetCardInfo()[0];
                poster = MediaManager.Instance.GetPosterName(title);
            }
            else if (item is LibraryItem.Series)
            {
                if (season != -1 && episode == -1)
                {
                    title = "Season " + (season + 1).ToString();
                    poster = MediaManager.Instance.GetSeasonPosterName(item, season);
                }
                else if (season != -1 && episode != -1)
                {
                    title = ((LibraryItem.Series)item).GetSeason(season).GetEpisode(episode).Name;
                    poster = MediaManager.Instance.GetEpisodePosterName(item, season, episode);
                }
                else
                {
                    title = item.GetCardInfo()[0];
                    poster = MediaManager.Instance.GetPosterName(title);
                }
            }

            lbl_name.Content = title;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(poster, UriKind.Relative);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            imageBox_poster.Source = bitmapImage;
            imageBox_poster.Stretch = Stretch.Uniform;
        }
    }
}
