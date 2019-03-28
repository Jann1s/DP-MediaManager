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

namespace DP_MediaManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Collection : Window
    {
        private MediaAPI.EntryInformation entry;
        public Collection()
        {
            InitializeComponent();
            entry = new MediaAPI.EntryInformation(47964);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(entry.GetName());
            Console.WriteLine(entry.GetGenre());
            Console.WriteLine(entry.GetPoster());
            Console.WriteLine(entry.GetRating());
            Console.WriteLine(entry.GetReleaseDate());
        }
    }
}
