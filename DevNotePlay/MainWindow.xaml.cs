using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Player
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));

           // CheckForUpdate();
        }


        #region SQUIRREL

        //async Task CheckForUpdate()
        //{

        //    using (var mgr = new UpdateManager("C:\\Projects\\MyApp\\Releases"))
        //    {
        //       await mgr.UpdateApp();

        //    }
        //}
        #endregion


        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Proxima_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Anterior_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            //setting
            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            dir = dir.Replace("file:\\", string.Empty);
            Process.Start(dir);
                  
        }

        private void btnRec_Click(object sender, RoutedEventArgs e)
        {
            var dir = LogApplication.Agent.GetCurrentDir();
            var exe = System.IO.Path.Combine(dir, @"Chrome\chrome-win\chrome.exe");

            Process.Start(exe);

        }
    }

    #region Marquee

    public class NegatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                return -((double)value);
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                return +(double)value;
            }
            return value;
        }
    }


    #endregion


}
