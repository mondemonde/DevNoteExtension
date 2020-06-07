using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using winForms = System.Windows.Forms;
using System.Windows.Input;
using System.Configuration;
using System.Xml;
using Player.Extensions;
using System.Diagnostics;
using System.IO;
using Player.Views.CustomControls;
using System.Reflection;
using Player.Enums;

namespace Player.Views
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

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = ConfigManager.MyConfigPath;
            _config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

            _appName = _config.AppSettings.Settings["AppName"].Value;

            InitializeControlValues();

            _updatedConfigValues = new Dictionary<string, string>();
        }

        private void SetDefaultValue(object sender, RoutedEventArgs e)
        {
            Button configItem = sender as Button;
            string configKey = configItem.Tag as string;
            ConfigSettingControl control = configItem.DataContext as ConfigSettingControl;
            PropertyInfo propertyInfo = typeof(ConfigurationDefaults).GetProperty(configKey);

            if (control.ConfigType == ConfigSettingTypes.Regular_CheckBox)
            {
                control.configCheckBox.IsChecked = propertyInfo.GetValue(null, null) as bool?;
            }
            else
            {
                string value = propertyInfo.GetValue(null, null) as string;
                ChangeConfigValue(configItem.DataContext, defaultValue: value, key: configKey);
            }
        }

        private void IsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(sender);
            if (_updatedConfigValues == null) return;

            CheckBox checkBox = sender as CheckBox;
            ConfigSettingControl configSetting = checkBox.DataContext as ConfigSettingControl;
            //string configKey = GetConfigKey(checkBox.Name, checkBox.GetType());
            string configKey = configSetting.ConfigKey;
            string configValue = checkBox.IsChecked.ToString();

            _updatedConfigValues[configKey] = configValue;
        }

        private void SaveDataTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ConfigSettingControl configItem = (ConfigSettingControl)sender;
            TextBox textBox = configItem.configTextBox_Manual;
            //TextBox textBox = (TextBox)sender;
            if (textBox.Tag as string == "TenantId")
            {
                if (textBox.Text.Length < textBox.MaxLength)
                {
                    MessageBox.Show("Tenant Id should contain exactly 36 characters." + Environment.NewLine +
                        "Format: 00112233-4455-6677-8899-aabbccddeeff");
                    return;
                }
            }

            string configKey = textBox.Tag as string;
            string configValue = textBox.Text;

            _updatedConfigValues[configKey] = configValue;
            Console.WriteLine((sender as ConfigSettingControl)?.ConfigKey);
        }

        private void ChangeDirectoryTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeConfigValue(sender, false, key: (sender as ConfigSettingControl).ConfigKey);
        }

        private void ChangeFileTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChangeConfigValue(sender, true, key: (sender as ConfigSettingControl).ConfigKey);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveConfig();
        }

        private void ChangeConfigValue(object sender, bool isFile = false, string defaultValue = null, string key = null)
        {
            //ConfigSettingControl configItem = (ConfigSettingControl)sender;
            TextBox textBox = GetConfigTextBox((ConfigSettingControl)sender);
            //winControls.TextBox textBox = configItem.configSettingControlGrid.Children.OfType<winControls.TextBox>().First();
            //winControls.TextBox textBox = (winControls.TextBox)sender;

            if (defaultValue != null)
            {
                string configKey = key;
                string configValue = defaultValue;

                _updatedConfigValues[configKey] = configValue;

                textBox.Text = configValue;
                return;
            }
            if (isFile)
            {
                winForms.OpenFileDialog fileBrowser = new winForms.OpenFileDialog();
                fileBrowser.InitialDirectory = Path.GetDirectoryName(textBox.Text);
                winForms.DialogResult dialogResult = fileBrowser.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    string configKey = key;
                    string configValue = fileBrowser.FileName;

                    _updatedConfigValues[configKey] = configValue;

                    textBox.Text = configValue;
                }
            }
            else
            {
                winForms.FolderBrowserDialog folderBrowser = new winForms.FolderBrowserDialog();
                if (File.Exists(textBox.Text))
                {
                    folderBrowser.SelectedPath = Path.GetFullPath(textBox.Text);
                }
                winForms.DialogResult dialogResult = folderBrowser.ShowDialog();

                if (dialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    string configKey = key;
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
            List<UIElementCollection> configTabs = new List<UIElementCollection>
            {
                MainFoldersControlGrid.Children,
                DesignerControlGrid.Children,
                RecordingsControlGrid.Children,
                DefaultsEventEntryControlGrid.Children,
                FolderEndpointsControlGrid.Children
            };

            foreach (var tab in configTabs)
            {
                foreach (var control in tab.OfType<ConfigSettingControl>())
                {
                    switch (control.ConfigType)
                    {
                        case ConfigSettingTypes.FileFolder_TextBox:
                            control.configTextBox_FileFolder.Text = _config.AppSettings.Settings[control.ConfigKey].Value;
                            break;
                        case ConfigSettingTypes.Regular_CheckBox:
                            control.configCheckBox.IsChecked = bool.Parse(_config.AppSettings.Settings[control.ConfigKey].Value);
                            break;
                        case ConfigSettingTypes.ManualEntry_TextBox:
                            control.configTextBox_Manual.Text = _config.AppSettings.Settings[control.ConfigKey].Value;
                            break;
                    }
                }
            }
        }

        private TextBox GetConfigTextBox(ConfigSettingControl control)
        {
            switch (control.ConfigType)
            {
                case ConfigSettingTypes.FileFolder_TextBox:
                    return control.configTextBox_FileFolder;
                case ConfigSettingTypes.ManualEntry_TextBox:
                    return control.configTextBox_Manual;
                default:
                    return null;
            }
        }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            e.Handled = InputValidators.PasteNotAllowed(e);
        }

        private void IntegerTextBoxChecker_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !InputValidators.NumbersOnly(e.Text);
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
           var dir =  LogApplication.Agent.GetCurrentDir();
            // opens the folder in explorer
            Process.Start(dir);
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabControl.SelectedIndex == tabControl.Items.Count - 1)
            {
                ShowAdvancedSettingsCheckbox.IsEnabled = false;
            }
            else ShowAdvancedSettingsCheckbox.IsEnabled = true;
        }
    }
}
