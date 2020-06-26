using Player.Extensions;
using Player.Models;
using Player.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Player.Views
{
    /// <summary>
    /// Interaction logic for AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        private readonly EventHeaderViewModel _eventHeaderViewModel;
        public AddEventWindow(Event @event)
        {
            InitializeComponent();

            _eventHeaderViewModel = new EventHeaderViewModel(@event);
            DataContext = _eventHeaderViewModel;
        }

        private void ViewEventTagLibrary_Click(object sender, RoutedEventArgs e)
        {
            EventLibraryWindow eventLibraryWindow = new EventLibraryWindow();
            eventLibraryWindow.Show();
            Close();
        }

        private void IntegerTextBoxChecker_PreviewTextInput(object sender, TextCompositionEventArgs e) { e.Handled = !InputValidators.NumbersOnly(e.Text); }

        private void SpaceNotAllowedTextBox_PreviewKeyDown(object sender, KeyEventArgs e) { e.Handled = InputValidators.SpaceNotAllowed(e); }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e) { e.Handled = InputValidators.PasteNotAllowed(e); }
    }
}
