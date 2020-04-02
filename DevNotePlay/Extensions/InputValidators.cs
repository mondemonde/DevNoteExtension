using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Player.Extensions
{
    public static class InputValidators
    {
        private static readonly Regex _regex = new Regex("[0-9]+");

        /// <summary>
        /// Bind to PreviewTextInput event of a TextBox to allow numbers only (0-9)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool NumbersOnly(string text)
        {
            return _regex.IsMatch(text);
        }

        /// <summary>
        /// Bind to PreviewExecuted event of a TextBox to prevent pasting
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool PasteNotAllowed(ExecutedRoutedEventArgs e)
        {
            return e.Command == ApplicationCommands.Paste;
        }
    }
}
