using Common;
using LogApplication.Common.Config;
using Newtonsoft.Json;
using Player.Extensions;
using Player.Models;
using Player.Services;
using Player.SharedViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace Player.ViewModels
{
    public class EventHeaderViewModel : INotifyPropertyChanged
    {
        private string AppName;
        private string RecordFileExtension;
        private string TenantId;
        private ProgressBarSharedView _progressBar;
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand UploadCommand { get; set; }
        public EventHeader EventToAdd { get; set; }

        public EventHeaderViewModel(Event @event = null)
        {
            ConfigManager config = new ConfigManager();
            AppName = config.GetValue("AppName");
            RecordFileExtension = config.GetValue("RecordFileExtension");
            TenantId = config.GetValue("TenantId");

            UploadCommand = new RelayCommand(OnUpload, CanUpload);
            CreateEvent(@event);
        }

        public void CreateEvent(Event @event)
        {
            EventToAdd = new EventHeader();
            EventToAdd.VersionNo = 1;
            EventToAdd.PropertyChanged += OnTargetUpdated;
            EventToAdd.TenantId = TenantId;

            if (@event != null)
            {
                EventToAdd.Domain = @event.Domain;
                EventToAdd.Department = @event.Department;
                EventToAdd.Descriptions = @event.Descriptions;
                EventToAdd.Tag = @event.Tag;
                NotUpdatingScript = false;
            }
        }

        private bool _notUpdatingScript = true;
        public bool NotUpdatingScript
        {
            get
            {
                return _notUpdatingScript;
            }
            set
            {
                _notUpdatingScript = value;
                RaisePropertyChanged("IsUpdatingScript");
            }
        }

        private async void OnUpload()
        {
            string headerFileName = Path.Combine(FileEndPointManager.Project2Folder, "header.json");
            using (StreamWriter file = File.CreateText(headerFileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, EventToAdd);
            }

            string recordJSDirectory = FileEndPointManager.DefaultPlayJsFile;
            string recordXMLDirectory = FileEndPointManager.DefaultLatestXMLFile;
            string recordHtmlDirectory = FileEndPointManager.DefaultLatestHtmlFile;

            List<string> fullPathsOfFilesToCompress = new List<string>
            {
                recordJSDirectory,
                recordXMLDirectory,
                recordHtmlDirectory,
                headerFileName
            };

            foreach (string file in fullPathsOfFilesToCompress)
            {
                if (!File.Exists(file))
                {
                    MessageBox.Show("There are no record files to save. Please open an existing recording or make a new one.",
                        AppName, MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            string eventToUploadFileName = Path.Combine(FileEndPointManager.Project2Folder, EventToAdd.FileName + "." + RecordFileExtension);

            ZipArchiveHelper.ArchiveFiles(fullPathsOfFilesToCompress, eventToUploadFileName);

            EventService eventTagService = new EventService();

            _progressBar = new ProgressBarSharedView("Uploading file. Please wait...");
            _progressBar.Show();
            var result = await eventTagService.CreateEvent(eventToUploadFileName);
            _progressBar.Close();

            MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);

            File.Delete(eventToUploadFileName);
            File.Delete(headerFileName);
            File.Delete(recordHtmlDirectory);
        }

        private bool CanUpload()
        {
            return EventToAdd.IsValid();
        }

        private void OnTargetUpdated(Object sender, EventArgs e)
        {
            UploadCommand.RaiseCanExecuteChanged();
        }

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
