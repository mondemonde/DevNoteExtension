using CodeceptSupport;
using CodeceptSupport.Mod;
using Common;
using Common.Policy;
using DevNote.Interface;
using DevNote.Interface.Common;
using DevNote.Web.Recorder;
using DevNoteCmdPlayer;
using DevNoteCmdPlayer2.Helpers;
using LogApplication.Common;
using LogApplication.Common.Commands;
using LogApplication.Common.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using TaskWaiter;

namespace Player
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window, IFrmDevNoteCmd
    {
        public void InvokeOnUiThreadIfRequired( Action action)
        {

            // Checking if this thread has access to the object.
            if (this.Dispatcher.CheckAccess())
            {
                // This thread has access so it can update the UI thread.
                //UpdateButtonUI(theButton);
                action.Invoke();
            }
            else
            {
                // This thread does not have access to the UI thread.
                // Place the update method on the Dispatcher of the UI thread.
                this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    action);
            }


            //if (this.InvokeRequired)
            //{
            //    BeginInvoke(action);
            //}
            //else
            //{
            //    action.Invoke();
            //}
        }

        public int ArmId {get;set;}
        public IChromeIdentity ChromePartner { get; set; }

        public CodeceptAction CurrentAction => throw new NotImplementedException();

        public string InitialChangeDirCmd {get;set;}
        public string InitialDirectory {get;set;}
        public bool IsArmReady {get;set;}
        public EnumTaskStatus IsAutoplayDone {get;set;}
        public bool IsAutoPlaying {get;set;}
        public bool IsAutoRun {get;set;}
        public bool IsHeadless {get;set;}
        public bool IsPlaying {get;set;}
        public bool IsSessionLifeSpan { get; set; }
        public string JSFile {get;set;}
        public CodeceptAction LastAction {get;set;}
        public List<CodeceptAction> MyActions {get;set;}
        public int MyRetry {get;set;}

        public string ProjectFolder => throw new NotImplementedException();

        public string RemoteDebuggerAddress {get;set;}
        public EnumPlayStatus Status {get;set;}
        public CmdToken Token {get;set;}
        public DateTime TimeStarted { get; private set; }

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;


        public MainWindow()
        {
            InitializeComponent();
            ToolTipService.ShowDurationProperty.OverrideMetadata(typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));


            // saveFileDialog1
            // 
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog1.Filter = "codecept|*.js|XML|*.xml|All Files|*.*";
            this.saveFileDialog1.Title = "Save DevNote Script (json)";
            // 
            // openFileDialog1
            // 
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();

            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1.DefaultExt = "\"json\"";
            this.openFileDialog1.Filter = "codecept|*.js|XML|*.xml|All Files|*.*";
            this.openFileDialog1.Title = "Browse Script Files (json)";
            // 

            ConfigManager config = new ConfigManager();         
            var chromeDefaultDownload = Path.GetDirectoryName(FileEndPointManager.DefaultPlayXMLFile);


            RecFileWatcher watcher = new RecFileWatcher();
            watcher.Player = this;
           

            var maxThreads = 4;
            // Times to as most machines have double the logic processers as cores
            ThreadPool.SetMaxThreads(maxThreads, maxThreads * 2);
            Console.WriteLine("Listening to folder endpoint.");
            Console.WriteLine("fileWatcher created.");

            IsSessionLifeSpan = true;
            // CheckForUpdate();

            txtVersion.Text = txtCaption.Text =string.Format("DevNotePlay™ version {0}", GetVersion());

        }

        string GetVersion()
        {
           var version =  DevAPI.GetVersion();
           return version.ToString();
        }
        #region SQUIRREL

        //async Task CheckForUpdate()
        //{

        //    using (var mgr = new UpdateManager("C:\\Projects\\MyApp\\Releases"))
        //    {
        //       await mgr.UpdateApp();

        //    }
        //}
        #endregion

        #region FILE ENDPOINT

        //This event adds the work to the Thread queue
        private void FileWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            ThreadPool.QueueUserWorkItem((o) => ProcessFile(e));
        }

        //This method processes your file, you can do your sync here
        private  void ProcessFile(System.IO.FileSystemEventArgs e)
        {
            // Based on the eventtype you do your operation
            switch (e.ChangeType)
            {
                case System.IO.WatcherChangeTypes.Changed:
                    Console.WriteLine($"File is changed: {e.Name}");
                    break;
                case System.IO.WatcherChangeTypes.Created:
                    Console.WriteLine($"File is created: {e.Name}");
                    Task.Run<bool>(async () => await TriggerPlay(e));

                   


                    break;
                case System.IO.WatcherChangeTypes.Deleted:
                    Console.WriteLine($"File is deleted: {e.Name}");
                    break;
                case System.IO.WatcherChangeTypes.Renamed:
                    Console.WriteLine($"File is renamed: {e.Name}");
                    Task.Run<bool>(async () => await TriggerPlay(e));

                    break;
            }
        }


       async Task<bool>  TriggerPlay(System.IO.FileSystemEventArgs e)
        {
            //step# 70 PLAY.TXT
            if (e.Name.ToLower() == "play.txt")
            {
                var endPointFolder = System.IO.Path.GetDirectoryName(FileEndPointManager.DefaultPlayXMLFile);


                Console.WriteLine("found play.txt");
               var span = DateTime.Now - TimeStarted;

                var latestFiles = System.IO.Directory.GetFiles(endPointFolder, "recor*.xml", System.IO.SearchOption.TopDirectoryOnly);
                var fileList = latestFiles.ToList();

                if (fileList.Count > 0)
                    return true;

                //dot restart let run for awhile
                if (span.TotalSeconds > 20)
                {
                    await  Play(true);
                    TimeStarted = DateTime.Now;
                }

                //delete play.txt

                //ConfigManager config = new ConfigManager();
                //var endPointFolder = config.GetValue("DefaultXMLFile");

                var txtFile = System.IO.Path.Combine(endPointFolder, "play.txt");
                System.IO.File.Delete(txtFile);
            }
            if (e.Name.ToLower().StartsWith("record"))
            {
                //copy and delete record.xml
                // record(1).xml  ,record(2).xml
                var endPointFolder = System.IO.Path.GetDirectoryName(FileEndPointManager.DefaultPlayXMLFile);

                //"*.exe|*.dll"
                var latestFiles = System.IO.Directory.GetFiles(endPointFolder, "recor*.xml", System.IO.SearchOption.TopDirectoryOnly);
                var fileList = latestFiles.ToList();



                //var latestXML = Path.Combine(endPointFolder, "latest_" + DateTime.Now.Ticks.ToString() + ".xml");
                var latestXML = System.IO.Path.Combine(endPointFolder, "latest.xml");

                if (System.IO.File.Exists(latestXML))
                    System.IO.File.Delete(latestXML);


                // FileEndPointManager.DefaultKATFile = latestXML;

                foreach (string file in fileList)
                {
                    try
                    {
                        System.IO.File.Copy(file, latestXML);
                        System.IO.File.Delete(file);

                    }
                    catch (Exception err)
                    {

                        LogApplication.Agent.LogError(err);
                    }
                }

            }

            return true;
        }

        #endregion



        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Proxima_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Anterior_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            //setting
            var dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            dir = dir.Replace("file:\\", string.Empty);
            Process.Start(dir);
                  
        }

        private void btnRec_Click(object sender, RoutedEventArgs e)
        {
            //STEP_.PLAYER #1 launch puppet with extension      
            CreateChrome();
        }

        public Task<bool> CellPlay(int RowIndex)
        {
            throw new NotImplementedException();
        }

        public void ConnectToChrome(IChromeIdentity chrome)
        {
            throw new NotImplementedException();
        }

        public static Process CmdExeForChrome;
        public static Process CmdExeForCodecept;


        public void CreateChrome()
        {
            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            string drive = System.IO.Path.GetPathRoot(dir);
            string driveLetter = drive.First().ToString();

            var param = string.Format("cd /{0} {1}\\CodeceptJs\\Project2", driveLetter, dir);


            //MyConsoleControlForChrome.WriteInput("node LaunchChromeExt.js", Color.AliceBlue, true);

            var batFolder = string.Format("{0}\\Bat", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var batPath = System.IO.Path.Combine(batFolder, "RunChromeExt.bat");
            var batTemplate = System.IO.File.ReadAllText(batPath);
            batTemplate = batTemplate.Replace("##Home##", param);

            //ConfigManager config = new ConfigManager();
            //var exe = config.GetValue("ChromeExe");

            var exe = FileEndPointManager.MyChromeViaRecorder;
            batTemplate = batTemplate.Replace("##.exe##", exe);


            var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeceptBatPath = System.IO.Path.Combine(codeceptjsFolder, "RunChromeExt.bat");


            if (System.IO.File.Exists(codeceptBatPath))
                System.IO.File.Delete(codeceptBatPath);

            System.IO.File.WriteAllText(codeceptBatPath, batTemplate);


            //step# 2 run bat file
            CmdExeForChrome = RunHelper.ExecuteCommandSilently(codeceptBatPath);

            return;
        }

        public Task<CmdParam> DoCmd(CmdParam command)
        {
            throw new NotImplementedException();
        }

        public bool IsCodeCeptReady()
        {
            throw new NotImplementedException();
        }

        //public Task<bool> Play(bool isRecording = false)
        //{
        //    throw new NotImplementedException();
        //    //int RevisionNumber = (int)(DateTime.UtcNow.Date - DateTime.UtcNow.AddHours(-12)).TotalSeconds;
        //}

        public async Task<bool> Play(bool isRecording = false)
        {

            // this.InvokeOnUiThreadIfRequired(() => ShowHelper());


            CloseCodeCeptJsWindow();

            // ConfigManager config = new ConfigManager();
            var defaultXML = FileEndPointManager.DefaultPlayXMLFile; // config.GetValue("DefaultXMLFile");


            if (isRecording)
            {
                var endPointFolder = Path.GetDirectoryName(FileEndPointManager.DefaultPlayXMLFile);

                //TODO check for error in UI
                //ActivateGroupBox(groupBoxRec);
                defaultXML = Path.Combine(endPointFolder, "latest.xml");
            }

            //groupBoxRec.Visible = true;
            //_HACK BtnPlay_Click
            #region---TEST ONLY: Compiler will  automatically erase this in RELEASE mode and it will not run if Global.GlobalTestMode is not set to TestMode.Simulation
#if OVERRIDE || OFFLINE

            Console.WriteLine("HACK-TEST -");
            //defaultXML = @"D:\_MY_PROJECTS\_DEVNOTE\_DevNote3\DevNote.Main\bin\Debug\KatalonRaw\ExternalRecordings\commonTestOrig.xml";
            //defaultXML = @"D:\_MY_PROJECTS\_DEVNOTE\_DevNote3\DevNote.Main\bin\Debug\KatalonRaw\ExternalRecordings\Tour.xml";
            // defaultXML = @"D:\_MY_PROJECTS\_DEVNOTE\_DevNote3\DevNote.Main\bin\Debug\KatalonRaw\ExternalRecordings\Inquiry.xml";
            // defaultXML = @"D:\_MY_PROJECTS\_DEVNOTE\_DevNote3\DevNote.Main\bin\Debug\KatalonRaw\ExternalRecordings\Inquiry.xml";
            // defaultXML = @"D:\_MY_PROJECTS\_DEVNOTE\WFH_Xamun_RawKatalon.xml";

            //defaultXML = @"D:\_KATALON\ERROR\latest.xml";
#endif
            #endregion //////////////END TEST

            //step# 5 load defaultXML
            Open_File(defaultXML);

            //dgActions.DataSource = actionSource;
            //dgActions.Refresh(); // Make sure this comes first
            //dgActions.Parent.Refresh(); // Make sure this comes second
            //flowMain.Refresh();

            RunCondeceptjsDefault();

            //Application.DoEvents();

            //int limit = MyRetry ;
            TaskWaiter.Conditions cond = new TaskWaiter.Conditions("Wait_CondceptJS_Console");
            await cond.WaitUntil(() => CmdExeForCodecept != null)
                .ContinueWith(x =>
                {

                    // WindowsHelper.FollowConsole(this, CmdExeForCodecept);


                });

            return true;
        }



        public Task<EnumPlayStatus> PlayStep()
        {
            throw new NotImplementedException();
        }

        public Task<IArmPlayer> Retry()
        {
            throw new NotImplementedException();
        }


        public void RunCondeceptjsDefault()
        {

            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            string drive = Path.GetPathRoot(dir);
            string driveLetter = drive.First().ToString();

            var param = string.Format("cd /{0} {1}\\CodeceptJs\\Project2", driveLetter, dir);

            //MyConsoleControlForChrome.WriteInput("node LaunchChromeExt.js", Color.AliceBlue, true);
            var batFolder = string.Format("{0}\\Bat", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var batPath = Path.Combine(batFolder, "RunCodeceptjs.bat");
            var batTemplate = File.ReadAllText(batPath);

            batTemplate = batTemplate.Replace("##Home##", param);
            var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeceptBatPath = Path.Combine(codeceptjsFolder, "RunCodeceptjs.bat");

            for (int i = 0; i < 10; i++)
            {
                codeceptBatPath = Path.Combine(codeceptjsFolder, "RunCodeceptjs" + i.ToString() + ".bat");

                if (File.Exists(codeceptBatPath))
                {
                    try
                    {
                        File.Delete(codeceptBatPath);
                        File.WriteAllText(codeceptBatPath, batTemplate);
                        break;

                    }
                    catch (Exception)
                    {

                        //  throw;
                    }
                }
                else
                {
                    File.WriteAllText(codeceptBatPath, batTemplate);
                    break;
                }

            }
            //step# 2 run bat file
            CmdExeForCodecept = RunHelper.ExecuteCommand(codeceptBatPath);
            return;
        }


        public void SetProjectFolder(string pathFolder)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void TestRunAsync(CodeceptAction action)
        {
            throw new NotImplementedException();
        }

        public string WriteCmd(string cmd)
        {
            throw new NotImplementedException();
        }

        private async void Anterior_Click_1(object sender, RoutedEventArgs e)
        {
            await Run(FileEndPointManager.DefaultPlayXMLFile);

        }

        private async Task<bool> Run(string filepath)
        {
            //STEP.Player #850 reset results.txt
            GlobalPlayer.ResetResult();
            var selectedJSXML = openFileDialog2.FileName = filepath;

            CloseCodeCeptJsWindow();

            //todo: make this LIstbox not gridview error on ui when using grid
            // ActivateGroupBox(groupBoxRec);
            //  groupBoxRec.Visible = true;

            //STEP_.Player #802 load defaultXML
            Open_File(selectedJSXML);

            string ext = Path.GetExtension(selectedJSXML);

            if (ext.ToLower() == ".js")
            {

                //update latest_test.js
                //STEP_.Player #803 get extension variables
                var runCmd = FileEndPointManager.ReadInputWFCmdJsonFile();

                var externalParam = runCmd.EventParameters;
                var internalResult = BotHttpClient.DevNoteGetParameters(runCmd.EventName).Content;//.ReadAsStringAsync();

                var internalParam = JsonConvert.DeserializeObject<Dictionary<string, string>>(internalResult);

                var script = File.ReadAllText(openFileDialog1.FileName);

                if (internalParam != null && externalParam != null)
                {

                    //STEP_.Player #804 CrossBreed the parameters
                    Dictionary<string, string> crossBreed = new Dictionary<string, string>();
                    foreach (var external in externalParam)
                    {

                        //internalParam-(arg.MappedTo_Input_X, arg.PropertyName.lower());
                        //ExternalParam---------------------------(PropertyName.tolower(), value); this is the external dictionary  crossed
                        //results to ---(arg.MappedTo_Input_X,value)
                        if (internalParam.ContainsValue(external.Key.ToLower()))
                        {
                            var internalP = internalParam.First(p => p.Value == external.Key.ToLower());
                            crossBreed.Add(internalP.Key, external.Value);
                        }
                    }


                    //STEP_.Player #803 insert variables
                    Interpreter it = new Interpreter();
                    //STEP.Player #804 Insert Variables
                    var selectedContent = it.InsertVariables(openFileDialog1.FileName, crossBreed).ToString();

                    script = selectedContent;
                    BotHttpClient.UpdateMainUI("InsertVariables " + Environment.NewLine + crossBreed.ToArray().ToString());


                }

                //  var selectedContent = File.ReadAllText(selectedJSXML);


                var dir = LogApplication.Agent.GetCurrentDir();
                dir = dir.Replace("file:\\", string.Empty);
                string drive = Path.GetPathRoot(dir);
                string driveLetter = drive.First().ToString();

                var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
                var codeceptTestPath = Path.Combine(codeceptjsFolder, "latest_test.js");


                if (File.Exists(codeceptTestPath))
                    File.Delete(codeceptTestPath);

                File.WriteAllText(codeceptTestPath, script);

                //play
                // dgActions.DataSource = actionSource;
                // dgActions.Refresh(); // Make sure this comes first
                //  dgActions.Parent.Refresh(); // Make sure this comes second
                //  flowMain.Refresh();

            }




            //STEP.Player #803 run _test.js using bat file
            RunCondeceptjsDefault();

            var started = DateTime.Now;

            //Application.DoEvents();
            TaskWaiter.Conditions cond = new TaskWaiter.Conditions("Wait_CondceptJS_Console");
            await cond.WaitUntil(() => (DateTime.Now - started).TotalSeconds > 5)
                .ContinueWith(x =>
                {

                    WindowsHelper.FollowConsole(CmdExeForCodecept);


                });


            //STEP.Player #855 //check result
            //wait for output
            //todo HERE...           

            var cond1 = new TaskWaiter.Conditions("wait_for_result.txt");
            await cond1.WaitUntil(() => AutoPlayPolicy.AssertPlayerResultExist(started) == true, 1000).ContinueWith(x =>
            {
                //setStatus(string.Format("Retried {0} times", MyRetry), EnumPlayStatus.Success);
                //Task.Delay(1000);
                //IsAutoPlaying = false;

                //Stop();

                //var result =  await AutoPlay();
                BotHttpClient.Log("Done.. Play codecept.");

                //step# 12 done EnumTaskStatus.DoneCodeCept
                //IsAutoplayDone = EnumTaskStatus.DoneCodeCept;


                //MyPayload.IsSuccess = true;
                //MyPayload.IsRespond = true;

                //step# 12.4 finished status
                //IsAutoplayDone = EnumTaskStatus.Finished;

                //not here.. yet it will retry
                // GlobalPlayer.CreateWFOutput("none");


            });



            //check result
            //STEP_.Player screenshot ERROR
            if (GlobalPlayer.IsFailedResult)
            {
                return false;// continue to retry
            }
            else
                return true; //no need to retry

        }


        void CloseCodeCeptJsWindow()
        {
            if (CmdExeForCodecept != null)
            {
                try
                {
                    //clears all windows
                    int handle = (int)CmdExeForCodecept.MainWindowHandle;
                    WindowsHelper.CloseWindow(handle);


                }
                catch (Exception err)
                {
                    LogApplication.Agent.LogError(err);
                    // throw;
                }
            }

            CmdExeForCodecept = new Process();

            Thread.Sleep(5000);

        }

        void CloseChromeWindow()
        {
            if (CmdExeForCodecept != null)
            {
                try
                {
                    //clears all windows
                    int handle = (int)CmdExeForChrome.MainWindowHandle;
                    WindowsHelper.CloseWindow(handle);


                }
                catch (Exception)
                {

                    // throw;
                }
            }

            CmdExeForChrome = null;

        }

        private void Open_File(string jsXMLFile)
        {
            //openFileDialog1.InitialDirectory = ProjectFolder;
            //openFileDialog1.Title = "Browse Script Files (json)";

            openFileDialog1.FileName = jsXMLFile;

            string file = openFileDialog1.FileName;
            string ext = System.IO.Path.GetExtension(file);

            if (ext.ToLower() == ".xml")
            {

                ReadXML(file);

            }
            else
            {
                saveFileDialog1.FileName = openFileDialog1.FileName;

                Interpreter it = new Interpreter();
                //STEP.Player #804 Insert Variables
                //it.InsertVariables(openFileDialog1.FileName);

                ////obsolete we may not need  it.MyActions;
                it.ReadJsonTestFile(openFileDialog1.FileName);

                // it.MyActions = new List<CodeceptAction>();
                //actionSource.DataSource = it.MyActions;
                //refreshList();
                ////end obsolete


               txtCaption.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            }


        }

        void ReadXML(string file)
        {
            //var file = @"D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\Katalon\Xamun.xml";// @"D:\_MY_PROJECTS\Mond\AIFS_Manager\CodeceptSupport\Katalon\test.xml";
            //run series of commands
            Interpreter it = new Interpreter();

            //step# 80 _Entry CONVERSION xml to codecept
            //_STEP_.Player _Entry CONVERSION xml to codecept
            it.ReadXmlFile(file);
            if (it.MyActions == null)
                return;

            //STEP.CodeCept #80 ACTIONS modifier
            //step# 81 mods..modify actions
            ClickModifier clickExt = new ClickModifier();
            it.Mod<ClickModifier>(clickExt);

            SendKeyModifier keyExt = new SendKeyModifier();
            it.Mod<SendKeyModifier>(keyExt);


            //step# 82 Declare VARIABLES
            FillFieldModifier fillFieldExt = new FillFieldModifier();
            it.Mod<FillFieldModifier>(fillFieldExt);

            //step# 83 identify Variables 
            VariableModifier variableList = new VariableModifier();
            it.Mod<VariableModifier>(variableList);


            //step# 83 assign Variables 
            AssignModifier variableExt = new AssignModifier();
            it.Mod<AssignModifier>(variableExt);

            //step# 84 finalize
            FinalModifier finalExt = new FinalModifier();
            it.Mod<FinalModifier>(finalExt);

            //MyActions = it.MyActions;
            //add summary
            MyActions = SummaryModifier.AddSummary(it, variableList.ListOfVariables);

            //this.actionSource.DataSource = MyActions;
            //refreshList();

            txtCaption.Text = "Dev Note Recorder -" +System.IO. Path.GetFileName(openFileDialog1.FileName);
            //saveFileDialog1.FileName = openFileDialog1.FileName;

            // var folder = Path.GetDirectoryName(jsXMLFile);


            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            string drive =System.IO.Path.GetPathRoot(dir);
            string driveLetter = drive.First().ToString();

            var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeceptTestPath =System.IO.Path.Combine(codeceptjsFolder, "latest_test.js");


            if (System.IO.File.Exists(codeceptTestPath))
               System.IO.File.Delete(codeceptTestPath);


            saveFileDialog1.FileName = codeceptTestPath;

            //step# 30 Save to default js file 
            toolStripButtonSave_Click(this, EventArgs.Empty);


        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {

            if (System.IO.Path.GetExtension(saveFileDialog1.FileName.ToLower()) == ".xml")
            {
                toolStripLabelSaveAs_Click(sender, e);
            }
            else
            {
                //save
                var list = MyActions; //actionSource.List;
                var length = list.Count;
                List<CodeceptAction> myList = new List<CodeceptAction>();

                string codes = string.Empty;

                for (int i = 0; i < length; i++)
                {
                    CodeceptAction a = (CodeceptAction)list[i];
                    //myList.Add(a);
                    if (string.IsNullOrEmpty(a.Script))
                        continue;

                    var code = a.Script.Trim();

                    while (code.Last() == ';')
                    {
                        code = code.Substring(0, code.Length - 1);
                    }

                    if (code.StartsWith("say('step#"))
                        codes = codes + string.Format("I.{0};\n", code);
                    else
                        codes = codes + string.Format("I.say('step#{0}');I.{1};\n", a.OrderNo.ToString(), code);


                }

                //var json = JsonConvert.SerializeObject(myList);
                // Create a file to write to.
                //string createText = json;

                //var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project1", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
                //var codeCeptConfigPath = Path.Combine(codeceptjsFolder, "codecept.conf.js");
                //string codeCeptConfigTemplate;
                //var codeCeptTestTemplate = File.ReadAllText(Path.Combine(codeceptjsFolder, "codecept.conf.template.txt"));

                //step# _8.4 config.GetValue("CodeceptTestTemplate");
                ConfigManager config = new ConfigManager();
                var codeCeptConfigPath = config.GetValue("CodeceptTestTemplate");
                var codeCeptTestTemplate = File.ReadAllText(codeCeptConfigPath);
                codeCeptTestTemplate = codeCeptTestTemplate.Replace("##steps##", codes);

                if (File.Exists(saveFileDialog1.FileName))
                    File.Delete(saveFileDialog1.FileName);

                //step# _8.5 save scenario file
                File.WriteAllText(saveFileDialog1.FileName, codeCeptTestTemplate);
               txtCaption.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            }


        }
        private void toolStripLabelSaveAs_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(saveFileDialog1.InitialDirectory))
                saveFileDialog1.InitialDirectory = ProjectFolder;
            //save as
            //saveFileDialog1.Title = "Save text Files";
            //saveFileDialog1.CheckFileExists = true;
            //saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "js";
            //saveFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() ==System.Windows.Forms.DialogResult.OK)
            {
                //textBox1.Text = saveFileDialog1.FileName;
                toolStripButtonSave_Click(sender, e);

                SetProjectFolder(System.IO.Path.GetDirectoryName(saveFileDialog1.FileName));
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            WindowsHelper.FollowConsole(this.Width,this.Height);
            if(CmdExeForCodecept!=null)
                  WindowsHelper.FollowConsole(CmdExeForCodecept);
        }
    }

    #region Marquee

    public class NegatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                return -((double)value);
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                return +(double)value;
            }
            return value;
        }
    }


    #endregion


}
