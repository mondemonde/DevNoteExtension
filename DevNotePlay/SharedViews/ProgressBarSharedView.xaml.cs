using System.Windows;

namespace Player.SharedViews
{
    /// <summary>
    /// Interaction logic for ProgressBarSharedView.xaml
    /// </summary>
    public partial class ProgressBarSharedView : Window
    {
        public ProgressBarSharedView(string caption)
        {
            InitializeComponent();
            StatusLabel.Content = caption;
        }
    }
}
