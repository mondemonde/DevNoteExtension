using Common.COMMANDS;
using DevNote.Interface;
using IntegrationEvents.Events.DevNote;
using LogApplication.Common.Commands;
using LogApplication.Common.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class FileEndPointManager
    {

        public static STEP_ Root { get; set; }

        public static String MyMainDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(myMainDirectory))
                {
                    //var dir = LogApplication.Agent.GetCurrentDir();
                    //dir = dir.Replace("file:\\", string.Empty);

                    ConfigManager config = new ConfigManager();
                    var dir = config.GetValue("MyMainFolder");


                    myMainDirectory = dir;
                }
                return myMainDirectory;
            }
        }



        static string _myCommonExeDirectory;
        public static string MyCommonExeDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_myCommonExeDirectory))
                {
                    ConfigManager config = new ConfigManager();
                    var file = config.GetValue("CommonExeFolder");
                    _myCommonExeDirectory = file;
                }
                return _myCommonExeDirectory;
            }

        }

        public static bool IsEventBusy
        {
            get
            {
                var files = Directory.GetFiles(MyWaitOneDirectory, "*.eve", SearchOption.TopDirectoryOnly);

                return files.Length > 0;
            }
        }

        public static String MyWaitOneDirectory
        {
            get
            {
                //STEP_.EVENT MyWaitOneDirectory
                if (string.IsNullOrEmpty(myWaitOneDirectory))
                {

                    var dir = string.Format("{0}\\Bat", FileEndPointManager.MyMainDirectory);
                    var dirWaitOne = System.IO.Path.Combine(dir, "WaitOne");

                    myWaitOneDirectory = dirWaitOne;
                }
                return myWaitOneDirectory;
            }
        }

        static string _myOutcomeFolder;
        public static String MyOutcomeFolder
        {
            get
            {
                //STEP_.EVENT MyWaitOneDirectory
                if (string.IsNullOrEmpty(_myOutcomeFolder))
                {

                    var dir = string.Format("{0}\\Bat", FileEndPointManager.MyMainDirectory);
                    var dirWaitOne = System.IO.Path.Combine(dir, "Outcome");

                    _myOutcomeFolder = dirWaitOne;
                }
                return _myOutcomeFolder;
            }
        }



        static string myEventDirectory;

        //TODO handle Event files
        public static String MyEventDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(myEventDirectory))
                {

                    var dir = string.Format("{0}\\Bat", FileEndPointManager.MyMainDirectory);
                    var dirEvents = System.IO.Path.Combine(dir, "Events");

                    myEventDirectory = dirEvents;
                }
                return myEventDirectory;
            }
        }



        public static string MyChromeViaRecorder
        {
            get
            {
                ConfigManager config = new ConfigManager();
                string exe = config.GetValue("ChromeExe");

                if (string.IsNullOrEmpty(exe))
                {
                    //get default directory
                    //D:\_MY_PROJECTS\_DEVNOTE\_DevNote4\DevNote.Web.Recorder\Chrome\chrome-win\chrome.exe
                    var currentDir = LogApplication.Agent.GetCurrentDir();
                    currentDir = currentDir.Replace("file:\\", string.Empty);


                    var dir = string.Format("{0}\\Chrome\\chrome-win", currentDir);
                    exe = System.IO.Path.Combine(dir, "chrome.exe");
                }
                return exe;
            }

        }

        public static string MyCodeceptTestTemplate
        {
            get
            {
                ConfigManager config = new ConfigManager();
                string template = config.GetValue("CodeceptTestTemplate");

                if (string.IsNullOrEmpty(template))
                {
                    //get default directory
                    //D:\_MY_PROJECTS\_DEVNOTE\_DevNote4\DevNote.Web.Recorder\Chrome\chrome-win\chrome.exe
                    var currentDir = LogApplication.Agent.GetCurrentDir();
                    currentDir = currentDir.Replace("file:\\", string.Empty);


                    var dir = string.Format("{0}\\CodeCeptJS\\Project2", currentDir);
                    template = System.IO.Path.Combine(dir, "template_test.txt");
                }
                return template;
            }

        }


        public static void CreateInputWF(RunWFCmdParam cmd, bool isOverwrite = false)
        {

            var stringContent = JsonConvert.SerializeObject(cmd); //new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");
            var file = Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFInput);

            if (File.Exists(file) && isOverwrite == false)
            {
                File.WriteAllText(file, stringContent);
                return;
            }
            else
            {
             ClearOutputWF();
             File.WriteAllText(file, stringContent);
            }

        }

        public static  async Task CreateEventInput(RunWFCmdParam cmd, bool isOverwrite = false)
        {

            var stringContent = JsonConvert.SerializeObject(cmd); //new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");

            var file = Path.Combine(FileEndPointManager.MyEventDirectory
                , DateTime.Now.Ticks.ToString() + EnumFiles.WFInput);

            if (File.Exists(file) && isOverwrite == false)
            {
                return;
            }
            else
            {
                // ClearOutputWF();
              await  Task.Factory.StartNew(() => File.WriteAllText(file, stringContent));
               // File.WriteAllText(file, stringContent);
            }

        }


        //must be 1 reference only
        public static async Task CreateOutputWF()
        {
            //STEP_.RESULT #99 CreateOutputWF
            var stringContent = File.ReadAllText(FileEndPointManager.InputWFFilePath);

            var cmd = ReadInputWFCmdJsonFile();
            var payload = cmd; //(RunWFCmdParam)cmd.Payload;

            var result = FileEndPointManager.ReadMyGrabValueFile();

            var @event = new DevNoteIntegrationEvent
            {
                GuidId = cmd.GuidId,
                EventParameters = cmd.EventParameters,
                EventName = cmd.EventName,
                OuputResponse = result,
                RetryCount = payload.RetryCount,
                ErrorCode = payload.ErrorCode
            };

            stringContent = JsonConvert.SerializeObject(@event);



            // var stringContent = JsonConvert.SerializeObject(cmd); //new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");
            var file = Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFOutput);



            //_HACK safe to delete 
            #region---TEST ONLY: Compiler will  automatically erase this in RELEASE mode and it will not run if Global.GlobalTestMode is not set to TestMode.Simulation
#if OVERRIDE || DEBUG

            //System.Diagnostics.Debug.WriteLine("HACK-TEST -");
            //await BotHttpClient.Log("FileEndPointManager.MyWaitOneDirectory:" + FileEndPointManager.MyWaitOneDirectory);
            //await BotHttpClient.Log("OuputResponse:" + result);


#endif
            #endregion //////////////END TEST



            await BotHttpClient.Log("OuputValue:" + result);



            File.WriteAllText(file, stringContent);

            if (!string.IsNullOrEmpty(cmd.EventFilePath))
            {

                    try
                    {
                        if (File.Exists(cmd.EventFilePath))
                            File.Delete(cmd.EventFilePath);
                    }
                    catch (Exception err)
                    {

                      await  BotHttpClient.Log(err.Message, true);
                    }
            }

            //STEP_.RESULT #6 save to OUTCOME
            var fName = Path.GetFileName(cmd.EventFilePath);
            fName = fName.Replace(EnumFiles.WFInput, EnumFiles.WFOutput);

            file = Path.Combine(FileEndPointManager.MyOutcomeFolder, fName);
            File.WriteAllText(file, stringContent);

            await BotHttpClient.Log("EventOutputStatus: " + Environment.NewLine + stringContent);


            //var fileIn = Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFInput);
            //if (File.Exists(fileIn))
            //    File.Delete(fileIn);
            ClearInputWF();

            //delete Eventfile
            if (!string.IsNullOrEmpty(cmd.EventFilePath))
            {
                try
                {
                    if (File.Exists(cmd.EventFilePath))
                    {
                        File.Delete(cmd.EventFilePath);
                    }
                }
                catch (Exception err)
                {

                    await BotHttpClient.Log(err.Message, true);
                }
               
            }



        }

        public static void ClearInputWF()
        {
            // var stringContent = JsonConvert.SerializeObject(cmd); //new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");
            var file = Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFInput);
            // File.WriteAllText(file, stringContent);
            if (File.Exists(file))
                File.Delete(file);

        }


        public static void ClearOutputWF()
        {
            // var stringContent = JsonConvert.SerializeObject(cmd); //new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");
            var file = Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFOutput);
            // File.WriteAllText(file, stringContent);
            if (File.Exists(file))
                File.Delete(file);

        }



        public static CmdParam ReadCmdJsonFile(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var cmd = JsonConvert.DeserializeObject<CmdParam>(json);
            return cmd;

        }

        public static string InputWFFilePath
        {
            get
            {
                 return Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFInput);

            }

        }
        public static string OutputWFFilePath
        {
            get
            {
                return Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFOutput);

            }

        }
        public static RunWFCmdParam ReadInputWFCmdJsonFile()
        {
            var cmd = new RunWFCmdParam
            {
                EventParameters = new Dictionary<string, string>()
            };

            if (File.Exists(InputWFFilePath))
            {
                var json = File.ReadAllText(InputWFFilePath);
                cmd = JsonConvert.DeserializeObject<RunWFCmdParam>(json);
               
            }
           
             return cmd;
            

        }

        public static DevNoteIntegrationEvent ReadOuputWFCmdJsonFile()
        {
            var @event = new DevNoteIntegrationEvent();
           
            if (File.Exists(OutputWFFilePath))
            {
                var json = File.ReadAllText(OutputWFFilePath);
                @event = JsonConvert.DeserializeObject<DevNoteIntegrationEvent>(json);

            }

            return @event;


        }


        public static bool IsWFBusy
        {
            get
            {
                var files = Directory.GetFiles(MyWaitOneDirectory, EnumFiles.WFOutput, SearchOption.TopDirectoryOnly);            
                bool isOuput = files.Length > 0;

                var filesIn = Directory.GetFiles(MyWaitOneDirectory, EnumFiles.WFInput, SearchOption.TopDirectoryOnly);

                bool isInput = filesIn.Length > 0;

                if (isInput && isOuput == false)
                {
                    return true;
                }
                else
                    return false;


            }
        }

        //public static bool IsWFStepDone
        //{
        //    get
        //    {
        //        // var files = Directory.GetFiles(MyWaitOneWFDirectory, "*.json", SearchOption.TopDirectoryOnly);
        //        //var file = System.IO.Path.Combine(MyWaitOneDirectory, EnumFiles.WFOutput);

        //        var result = GlobalDef.FrontWF.IsFrontWFShuttingDown==true;
        //        return result;
        //    }

            
        //}

        public static bool IsEventDone
        {
            get
            {
                // var files = Directory.GetFiles(MyWaitOneWFDirectory, "*.json", SearchOption.TopDirectoryOnly);
                var file = System.IO.Path.Combine(MyWaitOneDirectory, EnumFiles.EventResult);

                var result = File.Exists(file) || GlobalDef.FrontWF.IsFrontWFShuttingDown == true;
                return result;
            }


        }

        static string _defaultLatestXMLFile;
        public static string DefaultLatestXMLFile
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultLatestXMLFile))
                {
                    ConfigManager config = new ConfigManager();
                    var file = config.GetValue("DefaultLatestXMLFile");

                    if (string.IsNullOrEmpty(file))
                    {
                        _defaultLatestXMLFile = System.IO.Path.Combine(Project2Folder, "latest.xml");
                    }
                    else
                        _defaultLatestXMLFile = file;
                }
                return _defaultLatestXMLFile;
            }
        }

        static string _defaultPlayXMLFile;
        public static string DefaultPlayXMLFile
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultPlayXMLFile))
                {
                    ConfigManager config = new ConfigManager();
                    var file = config.GetValue("DefaultPlayXMLFile");

                    //STEP_.PLAYER CHROME DOWNLOAD FOLDER
                    //SERVER: use the main folder of devnote.main
                    if (string.IsNullOrEmpty(file))
                    {                 
                        //D:\_MY_PROJECTS\_DEVNOTE\_DevNote4\DevNote.Main\bin\Debug2\_EXE\Player\CodeCeptJS\Project2
                        //var dir = string.Format("{0}\\_EXE\\Player\\CodeCeptJS\\Project2", FileEndPointManager.MyMainDirectory);
                        _defaultPlayXMLFile = System.IO.Path.Combine(Project2Folder, "latest_test.js");
                    }
                    //CLIENT
                    else //if supplied used for stand alone player
                        _defaultPlayXMLFile = file;
                }
                return _defaultPlayXMLFile;
            }
        }

        static  string  _defaultScriptFolder;
        public static string DefaultScriptFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultScriptFolder))
                {
                    //ConfigManager config = new ConfigManager();
                    //var file = config.GetValue("DefaultXMLFile");

                   var file = Path.Combine(MyMainDirectory, "Scripts");
                    _defaultScriptFolder = file;
                }
                return _defaultScriptFolder;
            }
           
        }

        static string _defaultXAMLFolder;
        public static string DefaultXAMLFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultXAMLFolder))
                {
                    //ConfigManager config = new ConfigManager();
                    //var file = config.GetValue("DefaultXMLFile");

                    var file = Path.Combine(MyMainDirectory, "XAML");
                    _defaultXAMLFolder = file;
                }
                return _defaultXAMLFolder;
            }

        }



        #region ROOT Dynmic

        //    <add key="Project2EndPointFolder" 
        //value="D:\_MY_PROJECTS\_DEVNOTE\_DevNote4\DevNote.Web.Recorder\bin\Debug\CodeCeptJS\Project2\output\endpoint" />

        static string _project2EndPointFolder;
        public static String Project2EndPointFolder
        {
            get
            {
                //    if (Root == STEP_.PLAYER)
                //    {
                //        if (string.IsNullOrEmpty(_project2EndPointFolder))
                //        {
                //            //var dir = LogApplication.Agent.GetCurrentDir();
                //            //myMainDirectory = dir.Replace("file:\\", string.Empty);
                //            //var exeFolder1 = string.Format("{0}\\_EXE\\Player\\CodeCeptJS\\Project2\\output", myMainDirectory);

                //            _project2EndPointFolder = Path.Combine(Project2Folder,"output");
                //        }

                //    }
                //    else
                //    {
                //        //STEP_.EVENT Project2EndPointFolder
                //        if (string.IsNullOrEmpty(_project2EndPointFolder))
                //        {

                //            var dir = string.Format("{0}\\_EXE\\Player\\CodeCeptJS\\Project2\\output", FileEndPointManager.MyMainDirectory);
                //            var dirOutput = System.IO.Path.Combine(dir, "endpoint");

                //            _project2EndPointFolder = dirOutput;
                //        }
                //    }

                _project2EndPointFolder = Path.Combine(Project2Folder, "output");
                var dirOutput = System.IO.Path.Combine(_project2EndPointFolder, "endpoint");

                return _project2EndPointFolder;
            }
        }

        static string _project2Folder;
        public static string Project2Folder
        {
           
                get
                      {
                    if (Root == STEP_.PLAYER)
                    {
                        if (string.IsNullOrEmpty(_project2Folder))
                        {
                            var dir = LogApplication.Agent.GetCurrentDir();
                            myMainDirectory = dir.Replace("file:\\", string.Empty);
                            var exeFolder1 = string.Format("{0}\\CodeCeptJS\\Project2", MyMainDirectory);
                            _project2Folder = exeFolder1;
                        }

                    }
                    else
                    {
                        //STEP_.EVENT Project2EndPointFolder
                        if (string.IsNullOrEmpty(_project2Folder))
                        {

                            var dir = string.Format("{0}\\_EXE\\Player\\CodeCeptJS\\Project2", MyMainDirectory);
                           _project2Folder = dir;
                        }
                    }

                    return _project2Folder;
                }
            }
        public static string Latest_testJS()
        {
            var @result = string.Empty;
            var endPointFolder =   Path.Combine(MyMainDirectory, "XAML");


            var file = Path.Combine(endPointFolder, EnumFiles.MyGrabValue);

            if (File.Exists(file))
            {
                result = File.ReadAllText(file);

            }

            return result;



        }

        public static string ReadMyGrabValueFile()
        {
            var @result = string.Empty;
            var endPointFolder = Project2EndPointFolder;
            var file = Path.Combine(endPointFolder, EnumFiles.MyGrabValue);

            if (File.Exists(file))
            {
                result = File.ReadAllText(file);

            }

            return result;



        }


        #endregion







        private static string myMainDirectory;
        private static string myWaitOneDirectory;
        // private static string myOutputWFDirectory;

        #region SYNC CustomConfig

       public static void SyncCustomConfig()
        {
            //update config
            //STEP_.INIT UPdate ALL COnfig Sync Custom.Config

            var dir = LogApplication.Agent.GetCurrentDir();
            myMainDirectory = dir.Replace("file:\\", string.Empty);

            //set root folder for all
            ConfigManager config = new ConfigManager();
            config.SetValue(MyConfig.MyMainFolder.ToString(), myMainDirectory);



            var exeFolder1 = string.Format("{0}\\_EXE\\Receiver", myMainDirectory);
            var exePath = Path.Combine(exeFolder1, "EFCoreTransactionsReceiver.exe");
            config.SetValue(MyConfig.AzureServiceBusReceiver.ToString(),exePath);

           var exeFolder2 = string.Format("{0}\\_EXE\\Sender", myMainDirectory);
            exePath = Path.Combine(exeFolder2, "EFCoreTransactionsSender.exe");
            config = new ConfigManager();
            config.SetValue(MyConfig.AzureServiceBusSender.ToString(), exePath);


            var exeFolder3 = string.Format("{0}\\_EXE\\Designer", myMainDirectory);
            exePath = Path.Combine(exeFolder3, "BaiCrawler.exe");
            config.SetValue(MyConfig.DevNoteDesingnerExe.ToString(), exePath);


            var exeFolder4 = string.Format("{0}\\_EXE\\Player", myMainDirectory);
            exePath = Path.Combine(exeFolder4, "DevNote.Web.Recorder.exe");
            config.SetValue(MyConfig.DevNotePlayerExe.ToString(), exePath);



            var exeFolder5 = Project2EndPointFolder;
            config.SetValue(MyConfig.Project2EndPointFolder.ToString(), exeFolder5);




            //exeFolder = string.Format("{0}\\_EXE\\Player", myMainDirectory);
            //exePath = Path.Combine(exeFolder, "chrome.exe");
            //config = new ConfigManager();
            //config.SetValue(MyConfig.ChromeExe.ToString(), exePath);


            //copy to all Concern Exe
            var configFolder = myMainDirectory;
            var source = Path.Combine(configFolder   , "Custom.config");

            var dest1 = Path.Combine(exeFolder1  , "Custom.config");
            File.Copy(source, dest1, true);

            var dest2 = Path.Combine(exeFolder2, "Custom.config");
            File.Copy(source, dest2, true);

            var dest3 = Path.Combine(exeFolder3, "Custom.config");
            File.Copy(source, dest3, true);

            var dest4 = Path.Combine(exeFolder4, "Custom.config");
            File.Copy(source, dest4, true);

        }

        public static void SyncDatabaseConfig()
        {
            //D:\_MY_PROJECTS\_DEVNOTE\_DevNote4\DevNote.Main\bin\Debug2
            var dir = LogApplication.Agent.GetCurrentDir();
            myMainDirectory = dir.Replace("file:\\", string.Empty);
            var templatePath = Path.Combine(myMainDirectory, "Common.config.txt");
            var templateConfig = File.ReadAllText(templatePath);

            var actualPath = Path.Combine(myMainDirectory, "MyDBContext.sdf");
            var finalTxt = templateConfig.Replace("##DbFullPath##", actualPath);

            var myCommonConfig = Path.Combine(myMainDirectory, "common.config");
            File.WriteAllText(myCommonConfig, finalTxt.Trim());

            var source = myCommonConfig;

            var exeFolder1 = string.Format("{0}\\_EXE\\Receiver", myMainDirectory);
            var commonPath = Path.Combine(exeFolder1, "common.config");
            File.Copy(source, commonPath, true);




            var exeFolder2 = string.Format("{0}\\_EXE\\Sender", myMainDirectory);
            commonPath = Path.Combine(exeFolder2, "common.config");
            File.Copy(source, commonPath, true);


            var exeFolder3 = string.Format("{0}\\_EXE\\Designer", myMainDirectory);
            commonPath = Path.Combine(exeFolder3, "common.config");
            File.Copy(source, commonPath, true);


            var exeFolder4 = string.Format("{0}\\_EXE\\Player", myMainDirectory);
            commonPath = Path.Combine(exeFolder4, "common.config");
            File.Copy(source, commonPath, true);





        }


        public static void SyncLogConfig()
        {

            //initializeData="Logs\{ApplicationName}-{DateTime:yyyy-MM-dd}.log"
            var keyWord = @"{ApplicationName}-{DateTime:yyyy-MM-dd}.log";
            var dir = LogApplication.Agent.GetCurrentDir();
            myMainDirectory = dir.Replace("file:\\", string.Empty);
            var logFolder = Path.Combine(myMainDirectory, "Logs");
            var replaceWord = Path.Combine(logFolder, keyWord);


            var configTemplate = Path.Combine(myMainDirectory, "CommonLog.config.txt");
            var templateConfig = File.ReadAllText(configTemplate);

            var finalTxt = templateConfig.Replace(@"##LogPath##", replaceWord);

            var myCommonConfig = Path.Combine(myMainDirectory, "CommonLog.config");
            File.WriteAllText(myCommonConfig, finalTxt.Trim());

            var source = myCommonConfig;

            var exeFolder1 = string.Format("{0}\\_EXE\\Receiver", myMainDirectory);
            var commonPath = Path.Combine(exeFolder1, "CommonLog.config");
            File.Copy(source, commonPath, true);




            var exeFolder2 = string.Format("{0}\\_EXE\\Sender", myMainDirectory);
            commonPath = Path.Combine(exeFolder2, "CommonLog.config");
            File.Copy(source, commonPath, true);


            var exeFolder3 = string.Format("{0}\\_EXE\\Designer", myMainDirectory);
            commonPath = Path.Combine(exeFolder3, "CommonLog.config");
            File.Copy(source, commonPath, true);


            var exeFolder4 = string.Format("{0}\\_EXE\\Player", myMainDirectory);
            commonPath = Path.Combine(exeFolder4, "CommonLog.config");
            File.Copy(source, commonPath, true);





        }



        #endregion



    }
}
