using LogApplication.Common.Config;
using Player.Extensions;
using Player.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Player.Views
{
    /// <summary>
    /// Interaction logic for EventLibraryWindow.xaml
    /// </summary>
    public partial class EventLibraryWindow : Window
    {
        private readonly ConfigManager _configManager;
        private readonly string AppName;

        public EventLibraryWindow(MainWindow mainWindow = null)
        {
            InitializeComponent();

            _configManager = new ConfigManager();
            AppName = _configManager.GetValue("AppName");

            DataContext = new EventViewModel(mainWindow);
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
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }

        private void IntegerTextBoxChecker_PreviewTextInput(object sender, TextCompositionEventArgs e) { e.Handled = !InputValidators.NumbersOnly(e.Text); }

        private void SpaceNotAllowedTextBox_PreviewKeyDown(object sender, KeyEventArgs e) { e.Handled = InputValidators.SpaceNotAllowed(e); }
    }
}
