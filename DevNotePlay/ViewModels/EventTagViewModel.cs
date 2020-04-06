using LogApplication.Common.Config;
using Player.Models;
using Player.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Player.ViewModels
{
    public class EventTagViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<EventTag> EventTags { get; set; }

        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }

        private readonly string AppName;
        private EventTagService _eventTagService;

        public EventTagViewModel()
        {
            _eventTagService = new EventTagService();
            GetEventTags();

            UpdateCommand = new RelayCommand(OnUpdate, CanUpdate);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
            RefreshCommand = new RelayCommand(OnRefresh);

            ConfigManager configManager = new ConfigManager();
            AppName = configManager.GetValue("AppName");
        }

        private EventTag _selectedEventTag;

        public event PropertyChangedEventHandler PropertyChanged;

        public EventTag SelectedEvent
        {
            get
            {
                return _selectedEventTag;
            }
            set
            {
                _selectedEventTag = value;
                if (_selectedEventTag != null) _selectedEventTag.PropertyChanged += OnTargetUpdated;
                UpdateCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public void GetEventTags()
        {
            EventTags = _eventTagService.GetEvents();
            RaisePropertyChanged("EventTags");
        }

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

        private void OnTargetUpdated(Object sender, EventArgs e)
        {
            UpdateCommand.RaiseCanExecuteChanged();
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
