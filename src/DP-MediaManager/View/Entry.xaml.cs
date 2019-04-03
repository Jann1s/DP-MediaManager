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
    /// Interaction logic for Entry.xaml
    /// </summary>
    public partial class Entry : Page
    {
        private LibraryItem.Entry entry;

        public Entry()
        {
            InitializeComponent();

            string title = "";
            string poster = "";
            Stretch stretch = Stretch.Uniform;

            if (MediaManager.Instance.SelectedSeason != -1 && MediaManager.Instance.SelectedEpisode != -1)
            {
                title = MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedItem].GetCardInfo()[0];
                this.entry = MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedItem].GetDetails(MediaManager.Instance.SelectedSeason, MediaManager.Instance.SelectedEpisode);

                lbl_EpisodeNo.Visibility = Visibility.Visible;
                lbl_EpisodeNo_Text.Visibility = Visibility.Visible;
                lbl_EpisodeTitle.Visibility = Visibility.Visible;
                lbl_EpisodeTitle_Text.Visibility = Visibility.Visible;
                lbl_Season.Visibility = Visibility.Visible;
                lbl_SeasonText.Visibility = Visibility.Visible;

                lbl_EpisodeNo_Text.Content = (MediaManager.Instance.SelectedEpisode + 1).ToString();
                lbl_SeasonText.Content = (MediaManager.Instance.SelectedSeason + 1).ToString();
                lbl_EpisodeTitle_Text.Content = entry.Name;

                poster = MediaManager.Instance.GetEpisodePosterName(MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedItem], MediaManager.Instance.SelectedSeason, MediaManager.Instance.SelectedEpisode);
                stretch = Stretch.UniformToFill;
            }
            else
            {
                this.entry = MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedItem].GetDetails();
                title = entry.Name;
                poster = MediaManager.Instance.GetPosterName(title);
            }

            lbl_Title.Content = title;
            
            lbl_ReleaseDate.Content = entry.Release.ToString("dd.MM.yyyy");
            lbl_Description.Text = entry.Description;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(poster, UriKind.Relative);
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.EndInit();

            imageBox_poster.Source = bitmapImage;
            imageBox_poster.Stretch = stretch;

            if (entry.Cast != null)
            {
                lbl_Cast.Visibility = Visibility.Visible;
                lbl_CastName.Visibility = Visibility.Visible;
                lbl_CastRole.Visibility = Visibility.Visible;

                foreach (LibraryItem.Cast cast in entry.Cast)
                {
                    mainGrid.RowDefinitions.Add(new RowDefinition());
                    mainGrid.RowDefinitions[mainGrid.RowDefinitions.Count - 1].Height = new GridLength(50, GridUnitType.Pixel);

                    Label labelRole = new Label();
                    labelRole.Content = cast.Role;
                    labelRole.FontSize = 18;
                    labelRole.FontStyle = FontStyles.Italic;

                    Grid.SetColumn(labelRole, 1);
                    Grid.SetRow(labelRole, mainGrid.RowDefinitions.Count - 1);
                    Grid.SetColumnSpan(labelRole, 3);

                    mainGrid.Children.Add(labelRole);

                    Label labelName = new Label();
                    labelName.Content = cast.Firstname;
                    labelName.FontSize = 18;

                    Grid.SetColumn(labelName, 4);
                    Grid.SetRow(labelName, mainGrid.RowDefinitions.Count - 1);
                    Grid.SetColumnSpan(labelName, 3);

                    mainGrid.Children.Add(labelName);
                }
            }
        }
    }
}
