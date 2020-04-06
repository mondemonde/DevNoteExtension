using System;
using System.Globalization;
using System.Windows.Data;

namespace Player.Extensions
{
    /// <summary>
    /// Custom converter for string to int
    /// Specifically used when the user leaves int TextBoxes blank
    /// </summary>
    public class IntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //convert the int to a string:
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //convert the string back to an int here
            if (!int.TryParse(value.ToString(), out int i))
            {
                return 0;
            }
            else
            {
                return i;
            }
        }
    }
}
