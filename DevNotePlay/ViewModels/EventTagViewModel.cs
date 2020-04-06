using LogApplication.Common.Config;
using Player.Models;
using Player.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Player.ViewModels
{
    public class EventTagViewModel
    {
        public ObservableCollection<EventTag> EventTags { get; set; }

        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        private readonly string AppName;
        private readonly EventTagService _eventTagService;

        public EventTagViewModel()
        {
            _eventTagService = new EventTagService();
            GetEventTags();

            UpdateCommand = new RelayCommand(OnUpdate, CanUpdate);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);

            ConfigManager configManager = new ConfigManager();
            AppName = configManager.GetValue("AppName");
        }

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
                //UpdateCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public void GetEventTags()
        {
            //EventTagService _eventTagService = new EventTagService();
            EventTags = _eventTagService.GetEvents();
        }

        private async void OnUpdate()
        {
            //if (SelectedEvent == null)
            //{
            //    MessageBox.Show("Select an Event from the list first.", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

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

        private bool CanUpdate()
        {
            //return SelectedEvent != null;
            return true;
        }

        private bool CanDelete()
        {
            return SelectedEvent != null;
        }
    }
}
