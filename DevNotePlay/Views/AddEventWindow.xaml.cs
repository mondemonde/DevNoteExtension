using Player.Extensions;
using Player.ViewModels;
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

namespace Player.Views
{
    /// <summary>
    /// Interaction logic for AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        private readonly EventHeaderViewModel _eventHeaderViewModel;
        public AddEventWindow()
        {
            InitializeComponent();

            _eventHeaderViewModel = new EventHeaderViewModel();
            _eventHeaderViewModel.CreateBlankEvent();
            DataContext = _eventHeaderViewModel;
        }

        private void ViewEventTagLibrary_Click(object sender, RoutedEventArgs e)
        {
            EventTagLibraryWindow eventTagLibraryWindow = new EventTagLibraryWindow();
            eventTagLibraryWindow.ShowDialog();
        }

        private void IntegerTextBoxChecker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InputValidators.NumbersOnly(e.Text);
        }

        private void SpaceNotAllowedTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = InputValidators.SpaceNotAllowed(e);
        }

        private void UploadRecording_Click(object sender, RoutedEventArgs e)
        {
            _eventHeaderViewModel.SaveEvent();
        }
    }
}
