using Player.Enums;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Player.Views.CustomControls
{
    /// <summary>
    /// Interaction logic for ConfigSetting.xaml
    /// </summary>
    public partial class ConfigSettingControl : UserControl
    {
        public ConfigSettingControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ConfigSettingTypes ConfigType { get; set; } = 0;
        public string ConfigKey { get; set; }
        public string LabelCaption { get; set; }
        public int MaxLength { get; set; } = 0;

        private string LabelName
        {
            get { return ConfigKey + "Label"; }
        }
        private string TextBoxName
        {
            get { return ConfigKey + "TextBox"; }
        }
        private string ButtonName
        {
            get { return ConfigKey; }
        }

        public bool IsFileFolder
        {
            get { return DetermineConfigType(ConfigSettingTypes.FileFolder_TextBox); }
        }
        public bool IsManualEntry
        {
            get { return DetermineConfigType(ConfigSettingTypes.ManualEntry_TextBox); }
        }
        public bool IsCheckbox
        {
            get { return DetermineConfigType(ConfigSettingTypes.Regular_CheckBox); }
        }
        public bool IsNotCheckbox
        {
            get { return !IsCheckbox; }
        }


        public new event RoutedEventHandler MouseDoubleClick;
        public void OnDoubleClick(object sender, RoutedEventArgs e)
        {
            MouseDoubleClick?.Invoke(sender, e);
        }
        public event RoutedEventHandler Click;
        public void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(sender, e);
        }
        public event RoutedEventHandler Checked;
        public void OnCheck(object sender, RoutedEventArgs e)
        {
            Checked?.Invoke(sender, e);
        }
        public new event RoutedEventHandler LostFocus;
        public void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!IsManualEntry) return;
            LostFocus?.Invoke(sender, e);
        }
        public event RoutedEventHandler PreviewExecuted;
        public void OnPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!IsManualEntry) return;
            PreviewExecuted?.Invoke(sender, e);
        }
        public new event RoutedEventHandler PreviewTextInput;
        public void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsManualEntry) return;
            PreviewTextInput?.Invoke(sender, e);
        }

        private bool DetermineConfigType(ConfigSettingTypes value)
        {
            if (ConfigType == value) return true;
            else return false;
        }
    }
}
