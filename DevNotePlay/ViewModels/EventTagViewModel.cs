using BaiTextFilterClassLibrary;
using Common;
using LogApplication.Common.Config;
using Player.Models;
using Player.Services;
using Player.SharedViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Player.ViewModels
{
    public delegate void RefToFunction();

    public class EventTagViewModel: INotifyPropertyChanged
    {
        //Collections
        private ObservableCollection<EventTag> _eventTags;
        public ObservableCollection<EventTag> EventTags
        {
            get
            {
                return _eventTags;
            }
            set
            {
                _eventTags = value;
                RaisePropertyChanged("EventTags");
            }
        }

        private ObservableCollection<EventParameter> _eventParameters;
        public ObservableCollection<EventParameter> EventParameters
        {
            get
            {
                return _eventParameters;
            }
            set
            {
                _eventParameters = value;
                RaisePropertyChanged("EventParameters");
            }
        }

        private ObservableCollection<EventScriptFile> _eventScriptFiles;
        public ObservableCollection<EventScriptFile> EventScriptFiles
        {
            get
            {
                return _eventScriptFiles;
            }
            set
            {
                _eventScriptFiles = value;
                RaisePropertyChanged("EventScriptFiles");
            }
        }

        //TODO: Possibly move this to EventScriptFile as a property
        private ObservableCollection<string> _eventScriptVariables;
        public ObservableCollection<string> EventScriptVariables
        {
            get
            {
                return _eventScriptVariables;
            }
            set
            {
                _eventScriptVariables = value;
                RaisePropertyChanged("EventScriptVariables");
            }
        }

        //Event commands
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        //Parameter commands
        public RelayCommand CreateParameterCommand { get; set; }
        public RelayCommand UpdateParameterCommand { get; set; }
        public RelayCommand DeleteParameterCommand { get; set; }
        public RelayCommand RefreshParametersCommand { get; set; }
        public RelayCommand PlayScriptCommand { get; set; }

        private bool CreatingItem = false;
        private bool RowEditEndingLocker = true;
        private bool ScriptPlaying = false;
        private readonly string AppName;
        private EventTagService _eventTagService;
        private EventParameterService _eventParameterService;
        private ProgressBarSharedView _progressBar;
        private MainWindow _mainWindow;

        public EventTagViewModel(MainWindow mainWindow = null)
        {
            _eventTagService = new EventTagService();
            _eventParameterService = new EventParameterService();
            _mainWindow = mainWindow;

            UpdateCommand = new RelayCommand(OnUpdate, CanUpdate);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            RefreshCommand = new RelayCommand(OnRefresh);

            UpdateParameterCommand = new RelayCommand(OnUpdateParameter, CanUpdateParameter);
            DeleteParameterCommand = new RelayCommand(OnDeleteParameter, CanDeleteParameter);
            CreateParameterCommand = new RelayCommand(OnCreateParameter, CanCreateParameter);
            RefreshParametersCommand = new RelayCommand(OnRefreshParameters, CanRefreshParameters);
            PlayScriptCommand = new RelayCommand(OnPlayScript, CanPlayScript);

            ConfigManager configManager = new ConfigManager();
            AppName = configManager.GetValue("AppName");
        }

        public void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => GetEventTags()), DispatcherPriority.ContextIdle, null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Selected items
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
                CreatingItem = false;
                CreateParameterCommand.RaiseCanExecuteChanged();
                RefreshParametersCommand.RaiseCanExecuteChanged();
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
                PlayScriptCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged("SelectedEventScriptFile");
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
                CreatingItem = false;
                ScriptPlaying = false;
                PlayScriptCommand.RaiseCanExecuteChanged();
                CreateParameterCommand.RaiseCanExecuteChanged();
                RefreshParametersCommand.RaiseCanExecuteChanged();
                if (_selectedTab == 1 && _selectedEventTag != null)
                {
                    GetEventParameters(_selectedEventTag.Id);
                    GetEventScriptFiles();
                }
                else if (_selectedTab == 1 && _selectedEventTag == null)
                {
                    EventScriptFiles = null;
                    SelectedEventScriptFile = null;
                    EventParameters = null;
                }
            }
        }

        //Get methods
        public async void GetEventTags()
        {
            _progressBar = new ProgressBarSharedView("Loading Events library...");
            _progressBar.Show();
            EventTags = await _eventTagService.GetEvents();
            _progressBar.Close();
        }

        public async void GetEventParameters(int eventId)
        {
            _progressBar = new ProgressBarSharedView("Loading Event Parameters...");
            _progressBar.Show();
            EventParameters = await _eventParameterService.GetEventParameters(eventId);
            _progressBar.Close();
        }

        public void GetEventScriptFiles()
        {
            string sourcePath = _selectedEventTag.SourcePath;
            if (!File.Exists(sourcePath)) return;

            var xmlFileContent = File.ReadAllText(sourcePath);
            string[] delimeter = new string[] { "JSFullFIlePath=\"" };
            string[] split = xmlFileContent.Split(delimeter, StringSplitOptions.None);

            var splitList = split.ToList();
            ObservableCollection<EventScriptFile> scriptFiles = new ObservableCollection<EventScriptFile>();

            if (splitList.Count > 1)
            {
                for (int i = 1; i < splitList.Count; i++)
                {
                    EventScriptFile scriptFile = new EventScriptFile();
                    string path = splitList[i].Split('"').First();

                    string name = Path.GetFileNameWithoutExtension(path);
                    //name = (i).ToString() + ". " + name;

                    scriptFile.SourcePath = path;
                    scriptFile.Name = name;

                    scriptFiles.Add(scriptFile);
                }
            }
            EventScriptFiles = scriptFiles;

            foreach (var scriptFile in EventScriptFiles)
            {
                string path = scriptFile.SourcePath;

                //Get code from file
                if (File.Exists(path))
                {
                    scriptFile.Content = File.ReadAllText(path);
                }
                GetEventScriptFileVariables(path);
            }
            SelectedEventScriptFile = scriptFiles[0];
        }

        public void GetEventScriptFileVariables(string scriptSourcePath)
        {
            if (!File.Exists(scriptSourcePath)) return;

            int counter = 0;
            string line;

            //Read the file and display it line by line.  
            StreamReader file = new StreamReader(scriptSourcePath);
            EventScriptVariables = new ObservableCollection<string>();

            while ((line = file.ReadLine()) != null)
            {
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
            //RaisePropertyChanged("EventScriptVariables");
            file.Close();
            //System.Console.WriteLine("There were {0} lines.", counter);
        }

        //Parameter commands
        private void OnCreateParameter()
        {
            //Adds a blank entry to the EventParameters collection
            EventParameter eventParameter = new EventParameter();
            eventParameter.PropertyName = "";
            eventParameter.MappedTo_Input_X = "";
            eventParameter.WFProfileId = 0;
            EventParameters.Add(eventParameter);

            SelectedEventParameter = eventParameter;

            CreatingItem = true;
            CreateParameterCommand.RaiseCanExecuteChanged();
            UpdateParameterCommand.RaiseCanExecuteChanged();
            DeleteParameterCommand.RaiseCanExecuteChanged();
            RefreshParametersCommand.RaiseCanExecuteChanged();
        }

        public async void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs args)
        {
            //Main method responsible for creating Event Parameters
            //Only continue if CreatingItem flag is true
            if (!CreatingItem) return;
            if (SelectedEventParameter != null && RowEditEndingLocker)
            {
                //CommitEdit calls OnRowEditEnding again
                //RowEditingLocker set to false to prevent infinite loop
                if (SelectedEventParameter.Id != 0) return;
                RowEditEndingLocker = false;
                (sender as DataGrid).CommitEdit();
            }
            else return;

            //Only proceed with add if the created Event Parameter is valid
            if (!SelectedEventParameter.IsValid())
            {
                RowEditEndingLocker = true;
                return;
            }

            MessageBoxResult messageBoxResult = MessageBox.Show("Proceed with creation? Click Cancel to continue editing or No to discard changes.",
                AppName, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Cancel)
            {
                args.Cancel = true;
                RowEditEndingLocker = true;
                return;
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                //Refresh the list if No
                GetEventParameters(SelectedEvent.Id);
                args.Cancel = true;
            }
            else
            {
                //Create Event Parameter if Yes
                string result = await _eventParameterService.CreateEventParameter(SelectedEvent.Id, SelectedEventParameter);
                MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);
                GetEventParameters(SelectedEvent.Id);
            }

            CreatingItem = false;
            RowEditEndingLocker = true;
            CreateParameterCommand.RaiseCanExecuteChanged();
            UpdateParameterCommand.RaiseCanExecuteChanged();
            DeleteParameterCommand.RaiseCanExecuteChanged();
            RefreshParametersCommand.RaiseCanExecuteChanged();
        }

        private async void OnUpdateParameter()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to update this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            EventParameterService eventParameterService = new EventParameterService();
            string result = await eventParameterService.UpdateEventParameter(SelectedEvent.Id, SelectedEventParameter.Id, SelectedEventParameter);

            MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);
            GetEventParameters(SelectedEvent.Id);
        }

        private async void OnDeleteParameter()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to delete this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            EventParameterService eventParameterService = new EventParameterService();
            string result = await eventParameterService.DeleteEventParameter(SelectedEvent.Id, SelectedEventParameter.Id);

            MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);
            GetEventParameters(SelectedEvent.Id);
        }

        private void OnRefreshParameters()
        {
            GetEventParameters(SelectedEvent.Id);
        }

        private async void OnPlayScript()
        {
            ScriptPlaying = true;
            PlayScriptCommand.RaiseCanExecuteChanged();
            //MessageBox.Show(String.Format("Playing {0}!", SelectedEventScriptFile.FileNameWithExtension));

            EventParameterService eventParameterService = new EventParameterService();
            ProgressBarSharedView progressBar = new ProgressBarSharedView("Downloading script from server...");
            progressBar.Show();
            //Backup current latest_test.js
            FileEndPointManager.WriteBackupFile();
            string result = await eventParameterService.DownloadScriptFromServer(SelectedEvent.Id, SelectedEventScriptFile.ParentFolder);
            progressBar.Close();

            if (result == string.Empty && _mainWindow != null)
            {
                await _mainWindow.Run(FileEndPointManager.DefaultPlayXMLFile);
            }
            else
            {
                MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //Restore latest_test.js
            FileEndPointManager.RestoreBackupFile();
            ScriptPlaying = false;
            PlayScriptCommand.RaiseCanExecuteChanged();
        }

        private bool CanCreateParameter()
        {
            return SelectedEvent != null && !CreatingItem;
        }

        private bool CanUpdateParameter()
        {
            return SelectedEventParameter != null && SelectedEventParameter.IsValid() && !CreatingItem;
        }

        private bool CanDeleteParameter()
        {
            return SelectedEventParameter != null && !CreatingItem;
        }

        private bool CanRefreshParameters()
        {
            return SelectedEvent != null && !CreatingItem;
        }

        private bool CanPlayScript()
        {
            return !ScriptPlaying && SelectedEventScriptFile != null && SelectedEventScriptFile.Content != null;
        }

        //Event commands
        private async void OnUpdate()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to update this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            var eventTagService = new EventTagService();
            var result = await eventTagService.UpdateEventTag(SelectedEvent);

            MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);
            GetEventTags();
        }

        private async void OnDelete()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to delete this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            var eventTagService = new EventTagService();
            var result = await eventTagService.DeleteEventTag(SelectedEvent);

            MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);
            GetEventTags();
        }

        private void OnRefresh()
        {
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
