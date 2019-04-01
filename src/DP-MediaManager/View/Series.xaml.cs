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
    /// Interaction logic for Series.xaml
    /// </summary>
    public partial class Series : Page
    {
        private List<LibraryItem.Season> seasons;

        public Series()
        {
            InitializeComponent();

            LibraryItem.LibraryFactory series = MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedEntry];
            seasons = ((LibraryItem.Series)series).GetSeasons();

            int rows = (int)Math.Ceiling((double)seasons.Count / 4);
            int currentRow = 0;
            int currentColumn = 0;
            int currentItem = 0;

            for (int i = 0; i < rows; i++)
            {
                grid_View.RowDefinitions.Add(new RowDefinition());
                grid_View.RowDefinitions[i].Height = new GridLength(220, GridUnitType.Pixel);
            }

            int seasonCounter = 1;
            foreach (LibraryItem.Season item in seasons)
            {
                //item.GetPoster();
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "Season " + seasonCounter.ToString();
                textBlock.Tag = currentItem;
                textBlock.MouseDown += TextBlock_MouseDown;

                Grid.SetColumn(textBlock, currentColumn);
                Grid.SetRow(textBlock, currentRow);

                grid_View.Children.Add(textBlock);

                currentColumn++;
                currentItem++;
                seasonCounter++;
                if (currentColumn > 3)
                {
                    currentRow++;
                    currentColumn = 0;
                }
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tmpText = (TextBlock)sender;
            int index = (int)tmpText.Tag;

            Season season = new Season(index);
            NavigationService.Navigate(season);
        }
    }
}
