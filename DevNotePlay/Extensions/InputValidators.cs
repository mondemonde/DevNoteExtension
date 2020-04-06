using System.Text.RegularExpressions;
using System.Windows.Input;

namespace Player.Extensions
{
    public static class InputValidators
    {
        private static readonly Regex _regex = new Regex("[0-9]+");

        /// <summary>
        /// Bind to PreviewTextInput event of a TextBox to allow numbers only (0-9)
        /// Sample implementation:
        /// private void IntegerTextBoxChecker_PreviewTextInput(object sender, TextCompositionEventArgs e) { e.Handled = !InputValidators.NumbersOnly(e.Text); }
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool NumbersOnly(string text)
        {
            return _regex.IsMatch(text);
        }

        /// <summary>
        /// Add CommandManager.PreviewExecuted="TextBox_PreviewExecuted" to XAML definition of a TextBox to prevent pasting
        /// Sample implementation:
        /// private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e) {e.Handled = InputValidators.PasteNotAllowed(e);}
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool PasteNotAllowed(ExecutedRoutedEventArgs e)
        {
            return e.Command == ApplicationCommands.Paste;
        }

        /// <summary>
        /// Bind to PreviewKeyDown event of TextBox to prevent typing space
        /// Sample implementaion:
        /// private void SpaceNotAllowedTextBox_PreviewKeyDown(object sender, KeyEventArgs e) {e.Handled = InputValidators.SpaceNotAllowed(e);}
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool SpaceNotAllowed(KeyEventArgs e)
        {
            return e.Key == Key.Space;
        }
    }
}
