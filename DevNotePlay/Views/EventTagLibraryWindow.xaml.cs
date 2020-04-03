using LogApplication.Common.Config;
using Player.Extensions;
using Player.Models;
using Player.Services;
using Player.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Player.Views
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
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            _configManager = new ConfigManager();
            _eventTagService = new EventTagService();

            RefreshData();

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
                RefreshData();
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
                RefreshData();
                
            }
            else
            {
                MessageBox.Show("Delete failed.");
            }
        }

        private void IntegerTextBoxChecker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InputValidators.NumbersOnly(e.Text);
        }

        private void RefreshData()
        {
            EventTagViewModel eventTagViewModel;
            eventTagViewModel = new EventTagViewModel();
            eventTagViewModel.GetEventTags();

            this.DataContext = null;
            this.DataContext = eventTagViewModel;
        }
    }
}
