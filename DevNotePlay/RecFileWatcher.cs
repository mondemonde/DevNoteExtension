using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using DevNote.Web.Recorder.Helpers;
using DevNote.Interface.Common;
using Common;
using DevNoteCmdPlayer;

namespace DevNote.Web.Recorder
{
    public   class RecFileWatcher
    {
        public IFrmDevNoteCmd Player { get; set; }
        public static DateTime TimeStarted { get; set; }
        public string PlayFile { get; set; }

        public RecFileWatcher()
        {
            ConfigManager config = new ConfigManager();
            PlayFile = config.GetValue("PlayFile");
            //var endPointFolder =config.GetValue("DefaultXMLFile");

            var endPointFolder = FileEndPointManager.Project2Folder;

            FileSystemWatcher fileWatcher = new FileSystemWatcher(endPointFolder);

            //Enable events
            fileWatcher.EnableRaisingEvents = true;

            //Add event watcher
            fileWatcher.Changed += FileWatcher_Changed;
            fileWatcher.Created += FileWatcher_Changed;
            fileWatcher.Deleted += FileWatcher_Changed;
            fileWatcher.Renamed += FileWatcher_Changed;

            TimeStarted = DateTime.Now.AddSeconds(-10);
        }

        #region FILE ENDPOINT
        //This event adds the work to the Thread queue
        private void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((o) => ProcessFile(e));
        }

        //This method processes your file, you can do your sync here
        private void ProcessFile(FileSystemEventArgs e)
        {
            // Based on the eventtype you do your operation
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    Console.WriteLine($"File is changed: {e.Name}");
                    break;
                case WatcherChangeTypes.Created:
                    Console.WriteLine($"File is created: {e.Name}");
                    TriggerPlay(e);
                    break;
                case WatcherChangeTypes.Deleted:
                    Console.WriteLine($"File is deleted: {e.Name}");
                    break;
                case WatcherChangeTypes.Renamed:
                    Console.WriteLine($"File is renamed: {e.Name}");
                    TriggerPlay(e);
                    break;
            }
        }

        void TriggerPlay(FileSystemEventArgs e)
        {
            //step# 70 PLAY.TXT
            if (e.Name.ToLower() == PlayFile)
            {
                var endPointFolder = FileEndPointManager.Project2Folder;

                Console.WriteLine("found play.txt");
                var span = DateTime.Now - TimeStarted;

                var latestFiles = Directory.GetFiles(endPointFolder, "recor*.xml", SearchOption.TopDirectoryOnly);
                var fileList = latestFiles.ToList();

                if (fileList.Count > 0)
                    return;

                //dot restart let run for awhile
                if (span.TotalSeconds > 20)
                {
                    //var player = new frmDevNoteCmd();
                    Player.InvokeOnUiThreadIfRequired(() => Player.Play(true));
                    TimeStarted = DateTime.Now;
                }
                //delete play.txt

                //ConfigManager config = new ConfigManager();
                //var endPointFolder = config.GetValue("DefaultXMLFile");

                var txtFile = Path.Combine(endPointFolder, PlayFile);
                File.Delete(txtFile);
            }
            if (e.Name.ToLower().StartsWith("record"))
            {
                //copy and delete record.xml
                // record(1).xml  ,record(2).xml
                var endPointFolder = FileEndPointManager.Project2Folder;

                //"*.exe|*.dll"
                var latestFiles = Directory.GetFiles(endPointFolder, "recor*.xml",SearchOption.TopDirectoryOnly);
                var fileList = latestFiles.ToList();

                //var latestXML = Path.Combine(endPointFolder, "latest_" + DateTime.Now.Ticks.ToString() + ".xml");
                var latestXML = Path.Combine(endPointFolder, "latest.xml");

                if (File.Exists(latestXML))
                    File.Delete(latestXML);

                // FileEndPointManager.DefaultKATFile = latestXML;
                foreach (string file in fileList)
                {
                    try
                    {
                        File.Copy(file, latestXML);
                        File.Delete(file);

                    }
                    catch (Exception err)
                    {

                        LogApplication.Agent.LogError(err);
                    }
                }

            }
        }
        #endregion
    }
}
