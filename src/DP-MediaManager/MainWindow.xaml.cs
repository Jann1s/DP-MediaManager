using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DP_MediaManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private bool _allowDirectNavigation = false;
        //private NavigatingCancelEventArgs _navArgs = null;
        //private Duration _duration = new Duration(TimeSpan.FromSeconds(1));
        //private double _oldHeight = 0;

        private MediaManager manager;

        public MainWindow()
        {
            InitializeComponent();

            MediaManager tempManager = new MediaManager();
            MediaManager.Instance = tempManager;
            manager = MediaManager.Instance;
        }

        private void Btn_addEntry_Click(object sender, RoutedEventArgs e)
        {
            Window window = new View.ManageEntry();
            window.Show();
        }

        private void Btn_showCollection_Click(object sender, RoutedEventArgs e)
        {
            manager.SelectedItem = -1;
            manager.SelectedSeason = -1;
            manager.SelectedEpisode = -1;

            View.Collection collection = new View.Collection();
            frameMain.NavigationService.Navigate(collection);
        }

        
        /*
        private void frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (Content != null && !_allowDirectNavigation)
            {
                e.Cancel = true;

                _navArgs = e;
                _oldHeight = frameMain.RenderSize.Height;

                DoubleAnimation animation0 = new DoubleAnimation();
                animation0.From = frameMain.RenderSize.Height;
                animation0.To = 0;
                animation0.Duration = _duration;
                animation0.Completed += SlideCompleted;
                frameMain.BeginAnimation(HeightProperty, animation0);
            }
            _allowDirectNavigation = false;
        }

        private void SlideCompleted(object sender, EventArgs e)
        {
            _allowDirectNavigation = true;
            switch (_navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (_navArgs.Uri == null)
                        frameMain.Navigate(_navArgs.Content);
                    else
                        frameMain.Navigate(_navArgs.Uri);
                    break;
                case NavigationMode.Back:
                    frameMain.GoBack();
                    break;
                case NavigationMode.Forward:
                    frameMain.GoForward();
                    break;
                case NavigationMode.Refresh:
                    frameMain.Refresh();
                    break;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new DoubleAnimation();
                    animation0.From = 0;
                    animation0.To = _oldHeight;
                    animation0.Duration = _duration;
                    frameMain.BeginAnimation(HeightProperty, animation0);
                });
        }*/
    }
}
