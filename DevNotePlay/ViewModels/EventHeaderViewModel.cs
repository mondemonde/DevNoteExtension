using Common;
using LogApplication.Common.Config;
using Newtonsoft.Json;
using Player.Extensions;
using Player.Models;
using Player.Services;
using Player.SharedViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace Player.ViewModels
{
    public class EventHeaderViewModel
    {
        private string AppName;
        private string RecordFileExtension;
        private ProgressBarSharedView _progressBar;

        public RelayCommand UploadCommand { get; set; }
        public EventHeader EventToAdd { get; set; }

        public EventHeaderViewModel()
        {
            ConfigManager config = new ConfigManager();
            AppName = config.GetValue("AppName");
            RecordFileExtension = config.GetValue("RecordFileExtension");

            CreateBlankEvent();

            UploadCommand = new RelayCommand(OnUpload, CanUpload);
        }

        public void CreateBlankEvent()
        {
            EventToAdd = new EventHeader();
            EventToAdd.VersionNo = 1;
            EventToAdd.PropertyChanged += OnTargetUpdated;
        }

        private async void OnUpload()
        {
            string headerFileName = Path.Combine(FileEndPointManager.Project2Folder, "header.json");
            using (StreamWriter file = File.CreateText(headerFileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, EventToAdd);
            }

            string recordJSDirectory = FileEndPointManager.DefaultPlayXMLFile;
            string recordXMLDirectory = FileEndPointManager.DefaultLatestXMLFile;

            if (!File.Exists(recordJSDirectory) || !File.Exists(recordXMLDirectory))
            {
                //TODO: update this error message.
                MessageBox.Show("There are no record files to save. Please open an existing recording or make a new one.", AppName, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<string> fullPathsOfFilesToCompress = new List<string>
                {
                    recordJSDirectory,
                    recordXMLDirectory,
                    headerFileName
                };

                string eventToUploadFileName = Path.Combine(FileEndPointManager.Project2Folder, EventToAdd.FileName + "." + RecordFileExtension);

                ZipArchiveHelper.ArchiveFiles(fullPathsOfFilesToCompress, eventToUploadFileName);

                EventTagService eventTagService = new EventTagService();

                _progressBar = new ProgressBarSharedView("Uploading file. Please wait...");
                _progressBar.Show();
                var result = await eventTagService.CreateEvent(eventToUploadFileName);
                _progressBar.Close();

                MessageBox.Show(result, AppName, MessageBoxButton.OK, MessageBoxImage.Information);

                File.Delete(eventToUploadFileName);
            }
        }

        private bool CanUpload()
        {
            return EventToAdd.IsValid();
        }

        private void OnTargetUpdated(Object sender, EventArgs e)
        {
            UploadCommand.RaiseCanExecuteChanged();
        }
    }
}
