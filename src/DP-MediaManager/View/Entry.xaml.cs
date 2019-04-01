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

        public Entry(LibraryItem.Entry entry = null)
        {
            InitializeComponent();

            if (entry == null)
            {
                this.entry = MediaManager.Instance.GetLibrary()[MediaManager.Instance.SelectedEntry].GetDetails();
            }
            else
            {
                this.entry = entry;
            }

            lbl_Title.Content = this.entry.Name;
        }
    }
}
