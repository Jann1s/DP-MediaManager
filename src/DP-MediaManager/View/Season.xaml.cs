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
    /// Interaction logic for Seasons.xaml
    /// </summary>
    public partial class Season : Page
    {
        private List<LibraryItem.Entry> episodes;

        public Season(int index)
        {
            InitializeComponent();

            LibraryItem.LibraryFactory series = MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedEntry];
            episodes = ((LibraryItem.Series)series).GetSeasons()[index].GetEpisodes();

            int rows = (int)Math.Ceiling((double)episodes.Count / 4);
            int currentRow = 0;
            int currentColumn = 0;
            int currentItem = 0;

            for (int i = 0; i < rows; i++)
            {
                grid_View.RowDefinitions.Add(new RowDefinition());
                grid_View.RowDefinitions[i].Height = new GridLength(220, GridUnitType.Pixel);
            }

            int seasonCounter = 1;
            foreach (LibraryItem.Entry item in episodes)
            {
                //item.GetPoster();
                TextBlock textBlock = new TextBlock();
                textBlock.Text = item.Name;
                textBlock.Tag = item;
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
            LibraryItem.Entry entryObj = (LibraryItem.Entry)tmpText.Tag;

            Entry season = new Entry(entryObj);
            NavigationService.Navigate(season);
        }
    }
}
