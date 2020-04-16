using System.Linq;
using System.Windows;

namespace Player
{
    /// <summary>
    /// Interação lógica para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Window CurrentWindow
        {
            get { return Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive); }
        }
    }
}
