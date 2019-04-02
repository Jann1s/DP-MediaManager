using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DP_MediaManager.View
{
    /// <summary>
    /// Interaction logic for ManageEntry.xaml
    /// </summary>
    public partial class ManageEntry : Window
    {
        public ManageEntry()
        {
            InitializeComponent();
        }

        private void Btn_apply_Click(object sender, RoutedEventArgs e)
        {
            int id = getId(textBox_iD.Text);

            if (textBox_iD.Text.Contains("/tv/"))
            {
                MediaAPI.SeriesInformation seriesInformation = new MediaAPI.SeriesInformation(id);
                LibraryItem.LibraryFactory item = seriesInformation.GetEntryData();
                MediaManager.Instance.AddItem(item);
                MediaManager.Instance.CachePoster();
            }
            else
            {
                MediaAPI.MovieInformation movieInformation = new MediaAPI.MovieInformation(id);
                LibraryItem.LibraryFactory item = movieInformation.GetEntryData();
                MediaManager.Instance.AddItem(item);
                MediaManager.Instance.CachePoster();
            }

            this.Close();
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            int id = getId(textBox_iD.Text);
            MediaManager.Instance.DeleteItem(id);

            this.Close();
        }

        private int getId(string url)
        {
            string pattern = @"\/\d+-";
            Match reg = Regex.Match(url, pattern);
            string id = reg.Value.Substring(1, reg.Length - 2);

            return Int32.Parse(id);
        }
    }
}
