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
    public partial class MainWindow : Window
    {
        private MediaManager manager;

        public MainWindow()
        {
            InitializeComponent();
            manager = new MediaManager();
        }

        private void Btn_addEntry_Click(object sender, RoutedEventArgs e)
        {
            Window window = new View.ManageEntry();
            window.Show();
        }

        private void Btn_showCollection_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
