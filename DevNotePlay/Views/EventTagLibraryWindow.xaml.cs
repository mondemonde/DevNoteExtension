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
        private readonly ConfigManager _configManager;
        private readonly string AppName;

        public EventTagLibraryWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            _configManager = new ConfigManager();

            AppName = _configManager.GetValue("AppName");

            DataContext = new EventTagViewModel();
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

        private void IntegerTextBoxChecker_PreviewTextInput(object sender, TextCompositionEventArgs e) { e.Handled = !InputValidators.NumbersOnly(e.Text); }

        private void SpaceNotAllowedTextBox_PreviewKeyDown(object sender, KeyEventArgs e) { e.Handled = InputValidators.SpaceNotAllowed(e); }
    }
}
