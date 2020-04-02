using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using winControls = System.Windows.Controls;
using winForms = System.Windows.Forms;
using System.Windows.Input;
using System.Configuration;
using System.Xml;
using Player.Extensions;

namespace Player.SubWindows
{
    /// <summary>
    /// Interaction logic for ConfigureSettingsWindow.xaml
    /// </summary>
    public partial class ConfigureSettingsWindow : Window
    {
        private Dictionary<string, string> _updatedConfigValues;
        private Configuration _config;
        private string _appName;

        public ConfigureSettingsWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = ConfigManager.MyConfigPath;
            _config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            _appName = _config.AppSettings.Settings["AppName"].Value;

            InitializeControlValues();

            _updatedConfigValues = new Dictionary<string, string>();
        }

        private void IsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (_updatedConfigValues == null) return;

            winControls.CheckBox checkBox = (winControls.CheckBox)sender;
            string configKey = GetConfigKey(checkBox.Name, checkBox.GetType());
            string configValue = checkBox.IsChecked.ToString();

            _updatedConfigValues[configKey] = configValue;
        }

        private void SaveDataTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            winControls.TextBox textBox = (winControls.TextBox)sender;

            string configKey = GetConfigKey(textBox.Name, textBox.GetType());
            string configValue = textBox.Text;

            _updatedConfigValues[configKey] = configValue;
        }

        private void ChangeDirectoryTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeConfigValue(sender);
        }

        private void ChangeFileTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeConfigValue(sender, true);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveConfig();
        }

        private void ChangeConfigValue(object sender, bool isFile = false)
        {
            if (isFile)
            {
                winForms.OpenFileDialog fileBrowser = new winForms.OpenFileDialog();
                winForms.DialogResult dialogResult = fileBrowser.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    winControls.TextBox textBox = (winControls.TextBox)sender;

                    string configKey = GetConfigKey(textBox.Name, textBox.GetType());
                    string configValue = fileBrowser.FileName;

                    _updatedConfigValues[configKey] = configValue;

                    textBox.Text = configValue;
                }
            }
            else
            {
                winForms.FolderBrowserDialog folderBrowser = new winForms.FolderBrowserDialog();
                winForms.DialogResult dialogResult = folderBrowser.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    winControls.TextBox textBox = (winControls.TextBox)sender;

                    string configKey = GetConfigKey(textBox.Name, textBox.GetType());
                    string configValue = folderBrowser.SelectedPath;

                    _updatedConfigValues[configKey] = configValue;

                    if (configKey == "ScreenshotOnFail") configValue += @"\template.failed.png";

                    textBox.Text = configValue;
                }
            }
        }

        private void SaveConfig()
        {
            if (_updatedConfigValues.Count != 0)
            {
                //TODO: Use MyConfigPath on release
                string customConfigPath = ConfigManager.MyConfigPath;
                //string customConfigPath = @"C:\Projects\RPA\DevNoteExtension\DevNotePlay\Custom.config";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(customConfigPath);

                foreach (var configItem in _updatedConfigValues)
                {
                    foreach (XmlElement element in xmlDoc.DocumentElement)
                    {
                        if (element.Name.Equals("appSettings"))
                        {
                            foreach (XmlNode node in element.ChildNodes)
                            {
                                if (node.NodeType == XmlNodeType.Element && node.Attributes[0].Value.Equals(configItem.Key))
                                {
                                    node.Attributes[1].Value = configItem.Value;
                                    break;
                                }
                            }
                        }
                    }
                }
                xmlDoc.Save(customConfigPath);
                ConfigurationManager.RefreshSection("appSettings");

                var messageBoxResult = 
                    MessageBox.Show("Settings have been saved.", _appName, MessageBoxButton.OK, MessageBoxImage.Information);

                if (messageBoxResult == MessageBoxResult.OK)
                {
                    Close();
                }
            }
        }

        private void InitializeControlValues()
        {
            var mainFolderControls = MainFoldersControlGrid.Children;
            var designerControls = DesignerControlGrid.Children;
            var recordingsControls = RecordingsControlGrid.Children;
            var defaultsEventEntry = DefaultsEventEntryControlGrid.Children;
            var folderEndpoints = FolderEndpointsControlGrid.Children;

            foreach (var control in mainFolderControls.OfType<winControls.TextBox>())
            {
                control.Text = _config.AppSettings.Settings[GetConfigKey(control.Name, control.GetType())].Value;
            }
            foreach (var control in mainFolderControls.OfType<winControls.CheckBox>())
            {
                control.IsChecked = bool.Parse(_config.AppSettings.Settings[GetConfigKey(control.Name, control.GetType())].Value);
            }

            foreach (var control in designerControls.OfType<winControls.TextBox>())
            {
                control.Text = _config.AppSettings.Settings[GetConfigKey(control.Name, control.GetType())].Value;
            }
            foreach (var control in recordingsControls.OfType<winControls.TextBox>())
            {
                control.Text = _config.AppSettings.Settings[GetConfigKey(control.Name, control.GetType())].Value;
            }
            foreach (var control in defaultsEventEntry.OfType<winControls.TextBox>())
            {
                control.Text = _config.AppSettings.Settings[GetConfigKey(control.Name, control.GetType())].Value;
            }
            foreach (var control in folderEndpoints.OfType<winControls.TextBox>())
            {
                control.Text = _config.AppSettings.Settings[GetConfigKey(control.Name, control.GetType())].Value;
            }
        }

        private string GetConfigKey(string controlName, Type controlType)
        {
            return controlName.Replace(controlType.Name, "");
        }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = InputValidators.PasteNotAllowed(e);
        }

        private void IntegerTextBoxChecker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InputValidators.NumbersOnly(e.Text);
        }
    }
}
