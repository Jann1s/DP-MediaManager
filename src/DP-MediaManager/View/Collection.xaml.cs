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
    /// Interaction logic for Collection.xaml
    /// </summary>
    public partial class Collection : Page
    {
        public Collection()
        {
            InitializeComponent();

            List<LibraryItem.IMovie> movies = MediaManager.Instance.GetLibrary();
            
            int rows = (int)Math.Ceiling((double)movies.Count / 4);
            int currentRow = 0;
            int currentColumn = 0;
            int currentItem = 0;

            for (int i = 0; i < rows; i++)
            {
                grid_View.RowDefinitions.Add(new RowDefinition());
            }

            foreach (LibraryItem.IMovie movie in movies)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = movie.GetMovie().Name + " | " + movie.GetMovie().Description;
                textBlock.Tag = currentItem;
                textBlock.MouseDown += TextBlock_MouseDown;

                Grid.SetColumn(textBlock, currentColumn);
                Grid.SetRow(textBlock, currentRow);

                grid_View.Children.Add(textBlock);
                
                currentColumn++;
                currentItem++;

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
            MediaManager.Instance.SelectedEntry = (int)tmpText.Tag;

            //Check if its a series or movie HERE...
            Entry entry = new Entry();
            NavigationService.Navigate(entry);
        }
    }
}
