using Player.Services;
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
using System.Windows.Shapes;

namespace Player.SubWindows
{
    /// <summary>
    /// Interaction logic for EventTagLibraryWindow.xaml
    /// </summary>
    public partial class EventTagLibraryWindow : Window
    {
        private EventTagService _eventTagService;

        public EventTagLibraryWindow()
        {
            InitializeComponent();

            _eventTagService = new EventTagService();

            GetEventTags();
        }

        private void GetEventTags()
        {
            EventTagDataGrid.ItemsSource = _eventTagService.GetEventTagLibraryFromServer();
        }

        private void EventTagDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            if (!(headerName == "Domain" ||
                  headerName == "Id" ||
                  headerName == "Tag"))
            {
                e.Cancel = true;
            }
        }
    }
}
