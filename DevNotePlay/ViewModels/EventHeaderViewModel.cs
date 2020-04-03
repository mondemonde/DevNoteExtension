using Common;
using LogApplication.Common.Config;
using Newtonsoft.Json;
using Player.Extensions;
using Player.Models;
using Player.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Player.ViewModels
{
    public class EventHeaderViewModel
    {
        private string AppName;
        private string RecordFileExtension;

        public EventHeader EventToAdd { get; set; }

        public EventHeaderViewModel()
        {
            ConfigManager config = new ConfigManager();
            AppName = config.GetValue("AppName");
            RecordFileExtension = config.GetValue("RecordFileExtension");
        }

        public void CreateBlankEvent()
        {
            EventToAdd = new EventHeader();
            EventToAdd.Department = "Department of Health";
            EventToAdd.Descriptions = "This describes this item in detail.";
            EventToAdd.Domain = "DOH.gov";
            EventToAdd.FileName = "DoHRecording";
            EventToAdd.Tag = "Tag_to_Call_Recording";
            EventToAdd.VersionNo = 1;
        }

        public async void SaveEvent()
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
                await eventTagService.CreateEvent(eventToUploadFileName);
            }
        }
    }
}
