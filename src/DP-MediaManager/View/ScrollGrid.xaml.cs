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
    /// Interaction logic for ScrollGrid.xaml
    /// </summary>
    public partial class ScrollGrid : UserControl
    {
        private Page sourcePage;
        private ViewType type;

        public ScrollGrid(Page sourcePage)
        {
            InitializeComponent();
            this.sourcePage = sourcePage;

            int currentRow = 0;
            int currentColumn = 0;
            int currentItem = 0;

            if (MediaManager.Instance.SelectedItem != -1 && MediaManager.Instance.SelectedSeason == -1)
            {
                type = ViewType.Series;
            }
            else if (MediaManager.Instance.SelectedItem != -1 && MediaManager.Instance.SelectedSeason != -1)
            {
                type = ViewType.Season;
            }
            else
            {
                type = ViewType.Library;
            }

            if (type == ViewType.Series)
            {
                LibraryItem.LibraryFactory series = MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedItem];
                List<LibraryItem.Season> seasons = ((LibraryItem.Series)series).GetSeasons();
                PrepareGrid(seasons.Count);

                foreach (LibraryItem.Season item in seasons)
                {
                    CardView card = new CardView(series, currentItem);
                    card.Tag = currentItem;

                    card.MouseDown += Card_MouseDown;

                    Grid.SetColumn(card, currentColumn);
                    Grid.SetRow(card, currentRow);

                    grid_View.Children.Add(card);

                    currentColumn++;
                    currentItem++;

                    if (currentColumn > 3)
                    {
                        currentRow++;
                        currentColumn = 0;
                    }
                }
            }
            else if (type == ViewType.Season)
            {
                LibraryItem.LibraryFactory series = MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedItem];
                List<LibraryItem.Entry> episodes = ((LibraryItem.Series)series).GetSeasons()[MediaManager.Instance.SelectedSeason].GetEpisodes();
                PrepareGrid(episodes.Count);

                foreach (LibraryItem.Entry item in episodes)
                {
                    CardView card = new CardView(series, MediaManager.Instance.SelectedSeason, currentItem);
                    card.Tag = currentItem;

                    card.MouseDown += Card_MouseDown;

                    Grid.SetColumn(card, currentColumn);
                    Grid.SetRow(card, currentRow);

                    grid_View.Children.Add(card);

                    currentColumn++;
                    currentItem++;

                    if (currentColumn > 3)
                    {
                        currentRow++;
                        currentColumn = 0;
                    }
                }
            }
            else
            {
                List<LibraryItem.LibraryFactory> library = MediaManager.Instance.GetLibrary();
                PrepareGrid(library.Count);

                foreach (LibraryItem.LibraryFactory item in library)
                {
                    List<string> info = item.GetCardInfo();

                    CardView card = new CardView(item);
                    card.Tag = currentItem;
                    card.MouseDown += Card_MouseDown;

                    Grid.SetColumn(card, currentColumn);
                    Grid.SetRow(card, currentRow);

                    grid_View.Children.Add(card);

                    currentColumn++;
                    currentItem++;

                    if (currentColumn > 3)
                    {
                        currentRow++;
                        currentColumn = 0;
                    }
                }
            }            
        }

        private void PrepareGrid(int count)
        {
            int rows = (int)Math.Ceiling((double)count / 4);
            
            for (int i = 0; i < rows; i++)
            {
                grid_View.RowDefinitions.Add(new RowDefinition());
                grid_View.RowDefinitions[i].Height = new GridLength(220, GridUnitType.Pixel);
            }
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CardView tmpText = (CardView)sender;
            int index = (int)tmpText.Tag;
            List<LibraryItem.LibraryFactory> library = MediaManager.Instance.GetLibrary();

            if (type == ViewType.Library)
            {
                MediaManager.Instance.SelectedItem = index;
                MediaManager.Instance.SelectedSeason = -1;
                MediaManager.Instance.SelectedEpisode = -1;

                if (library[index] is LibraryItem.Movie)
                {
                    Entry entry = new Entry();
                    sourcePage.NavigationService.Navigate(entry);
                }
                else if (library[index] is LibraryItem.Series)
                {
                    Series series = new Series();
                    sourcePage.NavigationService.Navigate(series);
                }
            }
            else if (type == ViewType.Series)
            {
                MediaManager.Instance.SelectedSeason = index;
                MediaManager.Instance.SelectedEpisode = -1;

                Season season = new Season();
                sourcePage.NavigationService.Navigate(season);
            }
            else if (type == ViewType.Season)
            {
                MediaManager.Instance.SelectedEpisode = index;

                Entry entry = new Entry();
                sourcePage.NavigationService.Navigate(entry);
            }
        }
    }

    enum ViewType
    {
        Library,
        Series,
        Season
    }
}
