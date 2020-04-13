using BaiTextFilterClassLibrary;
using LogApplication.Common.Config;
using Player.Models;
using Player.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Player.ViewModels
{
    public class EventTagViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<EventTag> EventTags { get; set; }
        public ObservableCollection<EventParameter> EventParameters { get; set; }
        public List<EventScriptFile> EventScriptFiles { get; set; }

        //Event commands
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        //Parameter commands
        public RelayCommand UpdateParameterCommand { get; set; }
        public RelayCommand DeleteParameterCommand { get; set; }

        private readonly string AppName;
        private EventTagService _eventTagService;
        private EventParameterService _eventParameterService;

        //TODO: Possibly move this to EventScriptFile as a property
        public ObservableCollection<string> EventScriptVariables { get; set; }

        public EventTagViewModel()
        {
            _eventTagService = new EventTagService();
            _eventParameterService = new EventParameterService();
            GetEventTags();

            UpdateCommand = new RelayCommand(OnUpdate, CanUpdate);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            RefreshCommand = new RelayCommand(OnRefresh);

            UpdateParameterCommand = new RelayCommand(OnUpdateParameter, CanUpdateParameter);
            DeleteParameterCommand = new RelayCommand(OnDeleteParameter, CanDeleteParameter);

            ConfigManager configManager = new ConfigManager();
            AppName = configManager.GetValue("AppName");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private EventTag _selectedEventTag;
        public EventTag SelectedEvent
        {
            get
            {
                return _selectedEventTag;
            }
            set
            {
                _selectedEventTag = value;
                if (_selectedEventTag != null)
                {
                    _selectedEventTag.PropertyChanged += OnTargetEventUpdated;
                }
                UpdateCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedEvent");
            }
        }

        private EventScriptFile _selectedEventScriptFile;
        public EventScriptFile SelectedEventScriptFile
        {
            get
            {
                return _selectedEventScriptFile;
            }
            set
            {
                _selectedEventScriptFile = value;
            }
        }

        private EventParameter _selectedEventParameter;
        public EventParameter SelectedEventParameter
        {
            get
            {
                return _selectedEventParameter;
            }
            set
            {
                _selectedEventParameter = value;
                if (_selectedEventParameter != null)
                {
                    _selectedEventParameter.PropertyChanged += OnTargetParameterUpdated;
                }
                UpdateParameterCommand.RaiseCanExecuteChanged();
                DeleteParameterCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedEventParameter");
            }
        }

        private int _selectedTab;
        public int SelectedTab
        {
            get
            {
                return _selectedTab;
            }
            set
            {
                _selectedTab = value;
                if (_selectedTab == 1 && _selectedEventTag != null)
                {
                    GetEventParameters(_selectedEventTag.Id);
                    GetEventScriptFiles();
                }
            }
        }

        public void GetEventTags()
        {
            EventTags = _eventTagService.GetEvents();
            RaisePropertyChanged("EventTags");
        }

        public void GetEventParameters(int eventId)
        {
            EventParameters = _eventParameterService.GetEventParameters(eventId);
            RaisePropertyChanged("EventParameters");
        }

        public void GetEventScriptFiles()
        {
            string sourcePath = _selectedEventTag.SourcePath;
            if (!File.Exists(sourcePath)) return;

            var xmlFileContent = File.ReadAllText(sourcePath);
            string[] delimeter = new string[] { "JSFullFIlePath=\"" };
            string[] split = xmlFileContent.Split(delimeter, StringSplitOptions.None);

            var splitList = split.ToList();
            List<EventScriptFile> scriptFiles = new List<EventScriptFile>();

            if (splitList.Count > 1)
            {
                for (int i = 1; i < splitList.Count; i++)
                {
                    EventScriptFile scriptFile = new EventScriptFile();
                    string path = splitList[i].Split('"').First();

                    string name = Path.GetFileNameWithoutExtension(path);
                    name = (i).ToString() + ". " + name;

                    scriptFile.SourcePath = path;
                    scriptFile.Name = name;

                    scriptFiles.Add(scriptFile);
                }
            }
            EventScriptFiles = scriptFiles;
            RaisePropertyChanged("EventScriptFiles");

            foreach (var scriptFile in EventScriptFiles)
            {
                string path = scriptFile.SourcePath;
                GetEventScriptFileVariables(path);
            }
        }

        public void GetEventScriptFileVariables(string scriptSourcePath)
        {
            if (!File.Exists(scriptSourcePath)) return;
                //Set label text with script filename
                //FileName.Text = Path.GetFileName(scriptSourcePath);
                //Set textblock text with script content
                //TextArea.Text = File.ReadAllText(path);

            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            StreamReader file = new StreamReader(scriptSourcePath);
            EventScriptVariables = new ObservableCollection<string>();

            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine(line);
                counter++;

                var expressions = line.Split(new string[] { Keywords.declareVariable }, StringSplitOptions.None);
                //I.say('DECLARE');var
                //TIP: we only allow one varible declare per action line OR we only covert the first var
                if (expressions.Length > 1)
                {
                    //X='123';I.say('END_DECLARE')";I.fillField({id:'usernamebox'}
                    var expression = expressions[1].Split(';').First();

                    //x ='123'
                    //x
                    var xName = expression.Split('=').First().Trim();
                    if (!EventScriptVariables.Contains(xName))
                        EventScriptVariables.Add(xName);
                    //Set Data Source
                    //dgVariableColumn.DataSource = ListOfVariables;
                }
            }
            RaisePropertyChanged("EventScriptVariables");
            file.Close();
            //System.Console.WriteLine("There were {0} lines.", counter);
        }

        //Parameter commands
        private async void OnUpdateParameter()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to update this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            EventParameterService eventParameterService = new EventParameterService();
            string result = await eventParameterService.UpdateEventParameter(SelectedEvent.Id, SelectedEventParameter.Id, SelectedEventParameter);

            MessageBox.Show(result, AppName);
            GetEventParameters(SelectedEvent.Id);
        }

        private async void OnDeleteParameter()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to delete this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            EventParameterService eventParameterService = new EventParameterService();
            string result = await eventParameterService.DeleteEventParameter(SelectedEvent.Id, SelectedEventParameter.Id);

            MessageBox.Show(result, AppName);
            GetEventParameters(SelectedEvent.Id);
        }

        private bool CanUpdateParameter()
        {
            return SelectedEventParameter != null && SelectedEventParameter.IsValid();
        }

        private bool CanDeleteParameter()
        {
            return SelectedEventParameter != null;
        }

        //Event commands
        private async void OnUpdate()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to update this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            var eventTagService = new EventTagService();
            var result = await eventTagService.UpdateEventTag(SelectedEvent);
            //TODO: Make the error messages more meaningful by sending error details
            if (result == true)
            {
                MessageBox.Show("Event updated.");
                GetEventTags();
            }
            else
            {
                MessageBox.Show("Update failed.");
            }
        }

        private async void OnDelete()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to delete this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            var eventTagService = new EventTagService();
            var result = await eventTagService.DeleteEventTag(SelectedEvent);
            //TODO: Make the error messages more meaningful by sending error details
            if (result == true)
            {
                MessageBox.Show("Event deleted.");
                GetEventTags();
            }
            else
            {
                MessageBox.Show("Delete failed.");
            }
        }

        private void OnRefresh()
        {
            _eventTagService = new EventTagService();
            GetEventTags();
        }

        private bool CanUpdate()
        {
            return SelectedEvent != null && SelectedEvent.IsValid();
        }

        private bool CanDelete()
        {
            return SelectedEvent != null;
        }

        private void OnTargetEventUpdated(Object sender, EventArgs e)
        {
            UpdateCommand.RaiseCanExecuteChanged();
        }

        private void OnTargetParameterUpdated(Object sender, EventArgs e)
        {
            UpdateParameterCommand.RaiseCanExecuteChanged();
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
