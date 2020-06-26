using BaiTextFilterClassLibrary;
using Common;
using LogApplication.Common.Config;
using Player.Models;
using Player.Services;
using Player.SharedViews;
using Player.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace Player.ViewModels
{
    public delegate void RefToFunction();

    public class EventViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Collections
        private ObservableCollection<Event> _event;
        public ObservableCollection<Event> Events
        {
            get
            {
                return _event;
            }
            set
            {
                _event = value;
                RaisePropertyChanged("Events");
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
        public RelayCommand UploadCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        //Parameter commands
        public RelayCommand CreateParameterCommand { get; set; }
        public RelayCommand CommitCreateCommand { get; set; }
        public RelayCommand CancelCreateCommand { get; set; }
        public RelayCommand UpdateParameterCommand { get; set; }
        public RelayCommand DeleteParameterCommand { get; set; }
        public RelayCommand RefreshParametersCommand { get; set; }
        public RelayCommand PlayScriptCommand { get; set; }

        private bool ScriptPlaying = false;
        private readonly string AppName;
        private readonly string TenantId;
        private EventService _eventTagService;
        private EventParameterService _eventParameterService;
        private ProgressBarSharedView _progressBar;
        private MainWindow _mainWindow;

        public EventViewModel(MainWindow mainWindow = null)
        {
            _eventTagService = new EventService();
            _eventParameterService = new EventParameterService();
            _mainWindow = mainWindow;

            UpdateCommand = new RelayCommand(OnUpdate, CanUpdate);
            UploadCommand = new RelayCommand(OnUpload, CanUpdate);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            RefreshCommand = new RelayCommand(OnRefresh);

            UpdateParameterCommand = new RelayCommand(OnUpdateParameter, CanUpdateParameter);
            DeleteParameterCommand = new RelayCommand(OnDeleteParameter, CanDeleteParameter);
            CreateParameterCommand = new RelayCommand(OnCreateParameter, CanCreateParameter);
            CommitCreateCommand = new RelayCommand(OnCommitCreateParameter);
            CancelCreateCommand = new RelayCommand(OnCancelCreateParameter);
            RefreshParametersCommand = new RelayCommand(OnRefreshParameters, CanRefreshParameters);
            PlayScriptCommand = new RelayCommand(OnPlayScript, CanPlayScript);

            CreatingItem = false;
            ConfigManager configManager = new ConfigManager();
            TenantId = configManager.GetValue("TenantId");
            AppName = configManager.GetValue("AppName");
        }

        public void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => GetEventTags()), DispatcherPriority.ContextIdle, null);
        }

        private bool _creatingItem;
        public bool CreatingItem
        {
            get
            {
                return _creatingItem;
            }
            set
            {
                _creatingItem = value;
                RaisePropertyChanged("CreatingItem");
            }
        }

        //Selected items
        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                if (_selectedEvent != null)
                {
                    _selectedEvent.PropertyChanged += OnTargetEventUpdated;
                }
                CreatingItem = false;
                CreateParameterCommand.RaiseCanExecuteChanged();
                RefreshParametersCommand.RaiseCanExecuteChanged();
                UpdateCommand.RaiseCanExecuteChanged();
                UploadCommand.RaiseCanExecuteChanged();
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
                if (_selectedTab == 1 && _selectedEvent != null)
                {
                    GetEventParameters(_selectedEvent.Id);
                    //GetEventScriptFiles();
                }
                else if (_selectedTab == 1 && _selectedEvent == null)
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
            Events = await _eventTagService.GetEvents();
            _progressBar.Close();
        }

        public async void GetEventParameters(int eventId)
        {
            _progressBar = new ProgressBarSharedView("Loading Event Parameters...");
            _progressBar.Show();

            var result = await _eventParameterService.GetEventParameters(eventId);
            EventParameters = result.Item1;
            EventScriptFiles = result.Item2;
            if (EventScriptFiles != null)
            {
                SelectedEventScriptFile = EventScriptFiles.First();
                EventScriptVariables = SelectedEventScriptFile.Variables;
            }

            _progressBar.Close();
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
        
        private void OnCancelCreateParameter()
        {
            GetEventParameters(SelectedEvent.Id);
            CreatingItem = false;
            CreateParameterCommand.RaiseCanExecuteChanged();
            UpdateParameterCommand.RaiseCanExecuteChanged();
            DeleteParameterCommand.RaiseCanExecuteChanged();
            RefreshParametersCommand.RaiseCanExecuteChanged();
        }

        private async void OnCommitCreateParameter()
        {
            if (!(SelectedEventParameter.Id == 0 && SelectedEventParameter.IsValid()))
            {
                MessageBox.Show("Error. Make sure that the new parameter is selected and that Property Name and Variable fields have values.",
                    AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult messageBoxResult = MessageBox.Show("Proceed with creation? Click Cancel to continue editing or No to discard changes.",
                AppName, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Cancel)
            {
                return;
            }
            else if (messageBoxResult == MessageBoxResult.No)
            {
                OnCancelCreateParameter();
            }
            else
            {
                //Create Event Parameter if Yes
                string result = await _eventParameterService.CreateEventParameter(SelectedEvent.Id, SelectedEventParameter);
                MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);
                GetEventParameters(SelectedEvent.Id);
            }

            CreatingItem = false;
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
            //string result = await eventParameterService.DownloadScriptFromServer(SelectedEvent.Id, SelectedEventScriptFile.ParentFolder);
            string result = await eventParameterService.DownloadScriptFromServer(SelectedEvent.Id);
            progressBar.Close();

            if (result == string.Empty && _mainWindow != null)
            {
                await _mainWindow.Run(FileEndPointManager.DefaultPlayJsFile);
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

            SelectedEvent.TenantId = TenantId;
            var eventTagService = new EventService();
            var result = await eventTagService.UpdateEvent(SelectedEvent);

            MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);
            GetEventTags();
        }

        private void OnUpload()
        {
            AddEventWindow addEventWindow = new AddEventWindow(SelectedEvent);
            addEventWindow.Show();
        }

        private async void OnDelete()
        {
            var messageBoxResult = MessageBox.Show("Are you sure you want to delete this item?", AppName, MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No) return;

            var eventTagService = new EventService();
            var result = await eventTagService.DeleteEvent(SelectedEvent);

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

        //private bool CanUpload()
        //{
        //    //string recordJSDirectory = FileEndPointManager.DefaultPlayJsFile;
        //    //string recordXMLDirectory = FileEndPointManager.DefaultLatestXMLFile;
        //    //string recordHtmlDirectory = FileEndPointManager.DefaultLatestHtmlFile;

        //    //bool ScriptExists = File.Exists(recordJSDirectory);
        //    //bool XMLExists = File.Exists(recordXMLDirectory);
        //    //bool HTMLExists = File.Exists(recordHtmlDirectory);

        //    return SelectedEvent != null && SelectedEvent.IsValid();//&&
        //    //    ScriptExists && XMLExists && HTMLExists;
        //}

        private bool CanDelete()
        {
            return SelectedEvent != null;
        }

        private void OnTargetEventUpdated(Object sender, EventArgs e)
        {
            UpdateCommand.RaiseCanExecuteChanged();
            UploadCommand.RaiseCanExecuteChanged();
        }

        private void OnTargetParameterUpdated(Object sender, EventArgs e)
        {
            UpdateParameterCommand.RaiseCanExecuteChanged();
        }

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
