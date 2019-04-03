using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DP_MediaManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            MediaManager.Instance = new MediaManager();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            //Environment.Exit(0);
        }
    }
}
