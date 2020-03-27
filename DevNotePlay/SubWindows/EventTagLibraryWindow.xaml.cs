using LogApplication.Common.Config;
using Player.Models;
using Player.Services;
using Player.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Player.SubWindows
{
    /// <summary>
    /// Interaction logic for EventTagLibraryWindow.xaml
    /// </summary>
    public partial class EventTagLibraryWindow : Window
    {
        private readonly EventTagService _eventTagService;
        private readonly ConfigManager _configManager;
        private readonly string AppName;

        public EventTagLibraryWindow()
        {
            InitializeComponent();

            _configManager = new ConfigManager();
            _eventTagService = new EventTagService();
            EventTagViewModel eventTagViewModel = new EventTagViewModel();
            eventTagViewModel.GetEventTags();

            this.DataContext = eventTagViewModel;
            this.EventTagDataGrid.Items.Refresh();
            AppName = _configManager.GetValue("AppName");
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

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = EventTagDataGrid.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Select an Event from the list first.", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = await _eventTagService.UpdateEventTag((EventTag)selectedItem);
            //TODO: Make the error messages more meaningful by sending error details
            if (result == true)
            {
                MessageBox.Show("Event updated.");
            }
            else
            {
                MessageBox.Show("Update failed.");
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = EventTagDataGrid.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Select an Event from the list first.", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = await _eventTagService.DeleteEventTag((EventTag)selectedItem);
            //TODO: Make the error messages more meaningful by sending error details
            if (result == true)
            {
                MessageBox.Show("Event deleted.");
            }
            else
            {
                MessageBox.Show("Delete failed.");
            }
        }
    }
}
