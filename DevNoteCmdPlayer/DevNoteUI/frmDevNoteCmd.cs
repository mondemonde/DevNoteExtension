using AutoMapper;
using CodeceptSupport;
using CodeceptSupport.Mod;
using Common;
using DevNote.Interface;
using DevNote.Interface.Common;
using LogApplication.Common;
using LogApplication.Common.Commands;
using LogApplication.Common.Config;
using LogApplication.INFRA;
using MyCommonLib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskWaiter;
using WFHostingApplication.COMMANDS;
using System.Diagnostics;
using Microsoft.VisualBasic;
using DevNoteCmdPlayer2.Helpers;
using Common.COMMANDS;
using Newtonsoft.Json;
using Common.Policy;

namespace DevNoteCmdPlayer
{
    public partial class frmDevNoteCmd : Form, IArmPlayer, IFrmDevNoteCmd
    {
        public bool IsSessionLifeSpan { get; set; }
        public int ArmId { get; set; }

        public CmdToken Token { get; set; }
        public IChromeIdentity ChromePartner { get; set; }

        public EnumPlayStatus Status
        {
            get { return _playStatus; }
            set { _playStatus = value; }
        }

        public ConsoleControl.ConsoleControl MyConsoleControl;
        // public ConsoleControl.ConsoleControl MyConsoleControlForChrome;

        public static RunWFCmdParam MyPayload { get; set; }

        #region MENU
        private void frmDevNoteCmd_Load(object sender, EventArgs e)
        {

            this.addNewLibControl1.GoToLibrary += AddNewLibControl1_GoToLibrary;

            foreach (Control c in flowMain.Controls)
            {
                if (c.Name == "groupBoxRec")
                {
                    // flowMain.Controls.Add(groupBoxRec);
                    groupBoxLimbo.Controls.Add(c);
                }
            }

            ConfigManager config = new ConfigManager();
            //var defaultXML = config.GetValue("DefaultXMLFile");

            ////step# 5 load defaultXML
            //Open_File(defaultXML);

            UpdateUIState(EnumPlayStatus.Idle);

            MyConsoleControl.IsInputEnabled = true;
            MyConsoleControl.ShowDiagnostics = true;

            //step# 1 RUN I shell
            // toolStripButtonRunCMD_Click(sender, e);



            SetProjectFolder(FileEndPointManager.DefaultScriptFolder);

            // MyConsoleControl.Enabled = false;

            //step# 5.1  init FILE ENDPOINT
            //ConfigManager config = new ConfigManager();
            var endPointFolder = FileEndPointManager.Project2EndPointFolder;//config.GetValue("Project2EndPointFolder");

            FileSystemWatcher fileWatcher = new FileSystemWatcher(endPointFolder);


            //Enable events
            fileWatcher.EnableRaisingEvents = true;

            //Add event watcher
            fileWatcher.Changed += FileWatcher_Changed;
            fileWatcher.Created += FileWatcher_Changed;
            fileWatcher.Deleted += FileWatcher_Changed;
            fileWatcher.Renamed += FileWatcher_Changed;

            var maxThreads = 4;

            // Times to as most machines have double the logic processers as cores
            ThreadPool.SetMaxThreads(maxThreads, maxThreads * 2);



            Console.WriteLine("Listening to folder endpoint.");
            IsSessionLifeSpan = true;

            if (IsAutoRun)
            {
                // MyPayload = (RunWFCmdParam)GlobalDef.CurrentContainerCmd.Payload;
                //var cmd = JsonConvert.DeserializeObject<RunWFCmdParam>(GlobalDef.CurrentContainerCmd.Payload.ToString());
                var cmd = GlobalDef.CurrentCmd;//JsonConvert.DeserializeObject<RunWFCmdParam>(GlobalDef.CurrentCmd.ToString());

                MyPayload = cmd;//as RunWFCmdParam;

                if (string.IsNullOrEmpty(JSFile))
                {
                    JSFile = MyPayload.JSFullPath;
                }


                //STEP.Player #800 Autorun
                AutoRun(JSFile);


            }



        }

        private void AddNewLibControl1_GoToLibrary(object sender, EventArgs e)
        {
            openLibrary();


        }

        public string JSFile { get; set; }

        void ActivateGroupBox(Control newControl)
        {

            // return

            // BtnRec_MouseLeave(this, EventArgs.Empty);

            panel1.Visible = panel2.Visible = panel3.Visible = false;

            groupLib.Visible = false;


            newControl.Visible = true;

            foreach (Control c in flowMain.Controls)
            {
                if (c.Name == "groupBoxRec")
                {
                    // flowMain.Controls.Add(groupBoxRec);
                    groupBoxLimbo.Controls.Add(c);
                }
            }


            if (newControl.Name == "groupBoxRec")
            {
                flowMain.Controls.Add(groupBoxRec);
            }


            //this.Refresh();

            //flowMain.Refresh();
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
                    //Thread.Sleep(2000);
                    MyRetry += 1;
                    break;
                case WatcherChangeTypes.Deleted:
                    Console.WriteLine($"File is deleted: {e.Name}");
                    break;
                case WatcherChangeTypes.Renamed:
                    Console.WriteLine($"File is renamed: {e.Name}");
                    break;
            }
        }

        #endregion




        private void toolStripLabelOpen_Click(object sender, EventArgs e)
        {
            toolStripLabelOpen_Click1(sender, e);
        }


        //step# 8.8 run code test file
        private void ToolStripRunCodecept_Click(object sender, EventArgs e)
        {
            if (actionSource.Count == 0)
                return;

            toolStripButtonSave_Click(sender, e);
            DevNoteCmd.frmDevNoteCmd newArm = new DevNoteCmd.frmDevNoteCmd(this.Token, EnumRobotParts.CodeCeptStepArm.ToString());
            newArm.Show();

            ConfigManager config = new ConfigManager();
            bool isCefSharp = Convert.ToBoolean(config.GetValue("IsCefSharp"));

            //step# 8.9 run in project 2 
            newArm.InitCmdConsole();

            newArm.rtxtConsole.BackColor = Color.MidnightBlue;
            CodeceptTestRun(saveFileDialog1.FileName, newArm, isCefSharp);

        }

        [Obsolete]
        public void TestRunAsync(CodeceptAction action)
        {
            DevNoteCmd.frmDevNoteCmd newArm = new DevNoteCmd.frmDevNoteCmd(this.Token, EnumRobotParts.CodeCeptStepArm.ToString());
            newArm.Show();

            //TIP: ConfigManager
            ConfigManager config = new ConfigManager();
            bool isCefSharp = Convert.ToBoolean(config.GetValue("IsCefSharp"));

            //step# 8.9 run in project 2 
            newArm.InitCmdConsole();

            //step# 8.92 traslate script and save it to tempfile
            string translatedTestRunFile = string.Empty;





            //save it to project1 folder
            string tempfile = config.GetValue("CodeceptTestRunFile1");
            if (File.Exists(tempfile))
                File.Delete(tempfile);

            //update new script
            File.WriteAllText(tempfile, translatedTestRunFile);

            //run
            CodeceptTestRun(tempfile, newArm, isCefSharp);
        }


        //string  GetMyGrabValue()
        //{

        //    string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\JS\";

        //    var func = Edge.Func(File.ReadAllText(folder + "connect.js"));
        //    var debugUrl = await func(null);

        //    await Task.Delay(3000);

        //    LogApplication.Agent.LogInfo("debugUrl: " + debugUrl.ToString());

        //    //step# 5 this.RemoteDebuggerAddress conn
        //    //var folder = @"D:\_MY_PROJECTS\Mond\AIFS_Manager\BaiCrawler\bin\x86\Debug\Config\";
        //    folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\Config\";
        //    var conn = File.ReadAllText(Path.Combine(folder, "puppetConnection.txt"));
        //    this.RemoteDebuggerAddress = conn;

        //    return "";
        //}



        //always call this after InitCmdConsole so now ready to run in project2 
        public void CodeceptTestRun(string FilePath, DevNoteCmd.frmDevNoteCmd rightArm, bool isCefSharp = true)
        {
            //1. read script file
            var fileName = FilePath;//saveFileDialog1.FileName;
            var codeCeptTestFileContent = File.ReadAllText(fileName);

            string CodeceptTestRunFile2;
            ConfigManager config = new ConfigManager();
            CodeceptTestRunFile2 = config.GetValue("CodeceptTestRunFile2");

            if (File.Exists(CodeceptTestRunFile2))
                File.Delete(CodeceptTestRunFile2);

            //update new script
            File.WriteAllText(CodeceptTestRunFile2, codeCeptTestFileContent);

            //2. read and update config file based on project1
            string project1Config = config.GetValue("CodeceptConfigurationFile");
            var dir2 = Path.GetDirectoryName(CodeceptTestRunFile2);
            var fname = Path.GetFileName(project1Config);
            string project2Config = Path.Combine(dir2, fname);

            if (File.Exists(project2Config))
                File.Delete(project2Config);

            File.Copy(project1Config, project2Config, true);

            //TIP DIRectory
            //3. read and update step file for project2
            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            var codeceptjsFolder1 = string.Format("{0}\\CodeceptJs\\Project1", dir);
            var codeceptjsFolder2 = string.Format("{0}\\CodeceptJs\\Project2", dir);

            var stepFile1 = Path.Combine(codeceptjsFolder1, "steps_file.js");
            var stepFile2 = Path.Combine(codeceptjsFolder2, "steps_file.js");

            if (File.Exists(stepFile2))
                File.Delete(stepFile2);
            File.Copy(stepFile1, stepFile2, true);

            //4.run codecept
            fname = Path.GetFileName(CodeceptTestRunFile2);
            //right arm should already be initialized to run in project2
            //codeceptjs run github_test.js
            rightArm.WriteInput(string.Format("npx codeceptjs run {0} --debug", fname));
        }

        //always call this after InitCmdConsole so now ready to run in project2 
        public void CodeceptTestRunRetry(string codeCeptTestFileContent, DevNoteCmd.frmDevNoteCmd rightArm, bool isCefSharp = true)
        {
            //1. read script file
            //var fileName = FilePath;//saveFileDialog1.FileName;
            //var codeCeptTestFileContent = File.ReadAllText(fileName);

            string CodeceptTestRunFile2;
            ConfigManager config = new ConfigManager();
            CodeceptTestRunFile2 = config.GetValue("CodeceptTestRunFile2");

            if (File.Exists(CodeceptTestRunFile2))
                File.Delete(CodeceptTestRunFile2);

            //update new script
            File.WriteAllText(CodeceptTestRunFile2, codeCeptTestFileContent);

            //2. read and update config file based on project1
            string project1Config = config.GetValue("CodeceptConfigurationFile");
            var dir2 = Path.GetDirectoryName(CodeceptTestRunFile2);
            var fname = Path.GetFileName(project1Config);
            string project2Config = Path.Combine(dir2, fname);

            if (File.Exists(project2Config))
                File.Delete(project2Config);

            File.Copy(project1Config, project2Config, true);


            //3. read and update step file for project2
            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            var codeceptjsFolder1 = string.Format("{0}\\CodeceptJs\\Project1", dir);
            var codeceptjsFolder2 = string.Format("{0}\\CodeceptJs\\Project2", dir);

            var stepFile1 = Path.Combine(codeceptjsFolder1, "steps_file.js");
            var stepFile2 = Path.Combine(codeceptjsFolder2, "steps_file.js");

            if (File.Exists(stepFile2))
                File.Delete(stepFile2);
            File.Copy(stepFile1, stepFile2, true);




            fname = Path.GetFileName(CodeceptTestRunFile2);

            //right arm should already be initialized to run in project2
            //codeceptjs run github_test.js
            rightArm.WriteInput(string.Format("npx codeceptjs run {0} --debug", fname));
        }


        private async void ToolStripButtonNewProcess_Click_1(object sender, EventArgs e)
        {

            // await AutoPlay();


        }

        public int MyRetry { get; set; }
        EnumPlayStatus _playStatus { get; set; }
        //step# 7 start autoplay
        private async void ToolStripButtonNewProcess_ClickAsync(object sender, EventArgs e)
        {
            //step# 5.6 delete endpoint file result.txt
            //step# 5.1  init FILE ENDPOINT
            ConfigManager config = new ConfigManager();
            var endPointFolder = FileEndPointManager.Project2EndPointFolder;
            var resultTxt = Path.Combine(endPointFolder, "result.txt");
            if (File.Exists(resultTxt))
                File.Delete(resultTxt);

            //step# 5.7 do project2  
            _playStatus = EnumPlayStatus.Retrying;
            var newArm = await Retry();



            return;

            #region DISABLE I play------------------------------------------------

            IsAutoPlaying = true;
            IsAutoplayDone = EnumTaskStatus.Started;

            int length = actionSource.Count;
            int firstOrder = CurrentOrderNo;

            for (int i = firstOrder; i < length; i++)
            {

                Console.WriteLine("index:" + i.ToString() + "=> IsAutoPlaying = " + IsAutoPlaying.ToString());

                if (IsAutoPlaying == false)
                {
                    Stop();
                    return;
                }



                if (actionSource.Count == 0)
                    return;


                if (LastAction != null && LastAction.IsFailed)
                {
                    LastAction = null;

                }


                CurrentOrderNo = i;
                //LastAction = new CodeceptAction { IsFailed = true };
                Console.WriteLine("playing... Order# " + CurrentOrderNo.ToString());

                _playStatus = await PlayStep();

                Console.WriteLine("DONE... Order# " + CurrentOrderNo.ToString());

                if (CurrentAction == null)
                {
                    Stop();
                    return;
                }

                if (CurrentAction.IsFailed == false)
                {
                    //System.Threading.Thread.Sleep(300);
                    //delay
                    //System.Threading.Thread.Sleep(900);
                    Console.WriteLine("Delay after... Order# " + CurrentOrderNo.ToString());
                    await Task.Delay(900);
                }
                else//error
                {
                    //step# 5.7 do project2  
                    _playStatus = EnumPlayStatus.Retrying;
                    newArm = await Retry();
                    //newArm.SafeInvoke(w => w.Close());

                    //IsAutoPlaying = true;
                    //if (_playStatus == EnumPlayStatus.Faulted)
                    //      Stop();
                    IsAutoPlaying = false;
                    Stop();
                }

            }//end for each

            //var result =  await AutoPlay();
            Console.WriteLine("Done.. AutoPlay");

            //step# 12 done EnumTaskStatus.DoneCodeCept
            IsAutoplayDone = EnumTaskStatus.DoneCodeCept;


            Stop();

            #endregion
        }

        public EnumTaskStatus IsAutoplayDone { get; set; }

        //bool isRetrying { get; set; }
        async public Task<IArmPlayer> Retry()
        {


            //TsButtonCodeCeptRunStep_Click(this,EventArgs.Empty);
            //var newArm = createCodeceptRun(); //step run
            var newArm = createCodeceptRunAll(); //run all


            setStatus("Retrying...", EnumPlayStatus.Retrying);
            //5.7 wait until MyRetry> 1

            //AutoPlayPolicy.AssertMyGrabValueRemoved();


            //int limit = MyRetry ;
            //TaskWaiter.Conditions cond = new TaskWaiter.Conditions("wait_result.txt");
            //await cond.WaitUntil(() => AutoPlayPolicy.AssertRetry(MyRetry,limit)).ContinueWith(x =>
            //{
            //    setStatus(string.Format("Retried {0} times", MyRetry),EnumPlayStatus.Success);
            //    Task.Delay(3000);


            //});           
            //IsAutoPlaying = true;
            return newArm;
        }

        #endregion

        void setStatus(string statusMsg, EnumPlayStatus status)
        {
            this.SafeInvoke(w => w.toolStripStatusLabelConsoleState.Text = statusMsg);
            _playStatus = status;
        }

        #region CONSOLE UI
        public frmDevNoteCmd()
        {

            InitializeComponent();
            CreateConsole();
        }
        void CreateConsole()
        {
            MyConsoleControl = new ConsoleControl.ConsoleControl();

            this.groupBoxConsole.Controls.Add(this.MyConsoleControl);

            this.MyConsoleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyConsoleControl.IsInputEnabled = true;
            this.MyConsoleControl.Location = new System.Drawing.Point(0, 0);
            this.MyConsoleControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MyConsoleControl.Name = "consoleControl";
            this.MyConsoleControl.SendKeyboardCommandsToProcess = false;
            this.MyConsoleControl.ShowDiagnostics = true;
            this.MyConsoleControl.Size = new System.Drawing.Size(1028, 350);
            this.MyConsoleControl.TabIndex = 0;
            this.MyConsoleControl.OnConsoleOutput += new ConsoleControl.ConsoleEventHandler(this.consoleControl_OnConsoleOutput);
            this.MyConsoleControl.OnConsoleInput += new ConsoleControl.ConsoleEventHandler(ConsoleControl_OnConsoleInput);

            this.MyConsoleControl.InternalRichTextBox.BackColor = Color.DarkCyan; //this.splitContainer1.Panel2.BackColor;

            this.MyConsoleControl.InternalRichTextBox.Font = new Font("arial", 8, FontStyle.Regular);





            //  this.toolStripButtonRunCMD.Image = global::DevNoteCmdPlayer2.Properties.Resources.ConsoleControl;

        }

        private void ConsoleControl_OnConsoleInput(object sender, ConsoleControl.ConsoleEventArgs args)
        {


        }


        /// <summary>
        /// Handles the Click event of the toolStripButtonStopProcess control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonStopProcess_Click(object sender, EventArgs e)
        {
            //consoleControl.StopProcess();

            //  Update the UI state.
            UpdateUIState();
        }

        /// <summary>
        /// Updates the state of the UI.
        /// </summary>
        private void UpdateUIState(EnumPlayStatus status = EnumPlayStatus.Idle)
        {
            //  Update the state.
            if (MyConsoleControl.IsProcessRunning)
            {
                toolStripStatusLabelConsoleState.Text = "Running " + System.IO.Path.GetFileName(MyConsoleControl.ProcessInterface.ProcessFileName);
                status = EnumPlayStatus.Playing;
            }
            //  else
            //      toolStripStatusLabelConsoleState.Text = "Not Running";

            this.Text = "DevNote Recorder-" + status.ToString();
            this._playStatus = status;

        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonShowDiagnostics control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonShowDiagnostics_Click(object sender, EventArgs e)
        {
            MyConsoleControl.ShowDiagnostics = !MyConsoleControl.ShowDiagnostics;
            // UpdateUIState();
        }

        /// <summary>
        /// Handles the Tick event of the timerUpdateUI control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timerUpdateUI_Tick(object sender, EventArgs e)
        {
            // UpdateUIState();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonInputEnabled control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonInputEnabled_Click(object sender, EventArgs e)
        {
            // MyConsoleControl.IsInputEnabled = !MyConsoleControl.IsInputEnabled;
            //  UpdateUIState();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonRunCMD control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonRunCMD_Click(object sender, EventArgs e)
        {
            MyConsoleControl.StartProcess("cmd", null);
            //UpdateUIState();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonSendKeyboardCommandsToProcess control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonSendKeyboardCommandsToProcess_Click(object sender, EventArgs e)
        {
            MyConsoleControl.SendKeyboardCommandsToProcess = !MyConsoleControl.SendKeyboardCommandsToProcess;
            UpdateUIState();
        }

        /// <summary>
        /// Handles the Click event of the toolStripButtonClearOutput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButtonClearOutput_Click(object sender, EventArgs e)
        {
            MyConsoleControl.ClearOutput();
        }

        private void toolStripButtonRunCMD_Click_1(object sender, EventArgs e)
        {
            toolStripButtonRunCMD_Click(sender, e);
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            //this.SuspendLayout();
            //this.splitContainer1.Panel1.Controls.Remove(this.consoleControl);
            //CreateConsole();
            //this.ResumeLayout();



        }

        public void ConnectToChrome(IChromeIdentity chrome)
        {
            ChromePartner = chrome;
            Application.DoEvents();
            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            string drive = Path.GetPathRoot(dir);
            string driveLetter = drive.First().ToString();

            var param = string.Format("cd /{0} {1}\\CodeceptJs\\Project1", driveLetter, dir);

            //consoleControl.WriteInput("cd /d D:\\_ROBOtFRAMeWORK\\CodeceptsJs\\Project1", Color.AliceBlue, true);
            MyConsoleControl.WriteInput(param, Color.AliceBlue, true);
            #region CONFIG 

            //step# 7 codecept.conf.js
            var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project1", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeCeptConfigPath = Path.Combine(codeceptjsFolder, "codecept.conf.js");
            string codeCeptConfigTemplate;


            if (chrome.IsHeadless)
            {
                //xtodo Make this universal like step_file.js.template.js
                codeCeptConfigTemplate = File.ReadAllText(Path.Combine(codeceptjsFolder, "headless_codecept.conf.template.txt"));

            }
            else
            {
                codeCeptConfigTemplate = File.ReadAllText(Path.Combine(codeceptjsFolder, "codecept.conf.template.txt"));

            }

            //var folder = @"D:\_MY_PROJECTS\Mond\AIFS_Manager\BaiCrawler\bin\x86\Debug\Config\";
            //var folder = @"D:\_MY_PROJECTS\Mond\AIFS_Manager\BaiCrawler\bin\x86\Debug\Config\";
            //var conn = File.ReadAllText(Path.Combine(folder, "puppetConnection.txt"));

            codeCeptConfigTemplate = codeCeptConfigTemplate.Replace("##url##", chrome.RemoteDebuggerAddress);
            if (File.Exists(codeCeptConfigPath))
                File.Delete(codeCeptConfigPath);

            File.WriteAllText(codeCeptConfigPath, codeCeptConfigTemplate);

            //step# 5.3 set step_file.js            
            var fileTemplateFolder = string.Format("{0}\\CodeceptJs", dir);
            var fileTemplate = Path.Combine(fileTemplateFolder, "steps_file.js.template.js");
            var contentTemplate = File.ReadAllText(fileTemplate);

            //do replacement
            //not needed..
            //contentTemplate = contentTemplate.Replace("##EndPointFolder##", endPointFolder);

            //step# 5.4 create steps_file.js 
            codeCeptConfigPath = Path.Combine(codeceptjsFolder, "steps_file.js");
            if (File.Exists(codeCeptConfigPath))
                File.Delete(codeCeptConfigPath);
            File.WriteAllText(codeCeptConfigPath, contentTemplate);


            #endregion

            MyConsoleControl.WriteInput("npx codeceptjs shell", Color.AliceBlue, true);
        }

        private void toolStripLabel2_Click_1(object sender, EventArgs e)
        {
            #region CONFIG 
            //var codeceptjsFolder = @"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            //var codeCeptConfigPath = codeceptjsFolder + "codecept.conf.js";
            //var codeCeptConfigTemplate = File.ReadAllText(codeceptjsFolder + "codecept.conf.template.txt");

            //var folder = @"D:\_MY_PROJECTS\Mond\AIFS_Manager\BaiCrawler\bin\x86\Debug\Config\";
            //var conn = File.ReadAllText(folder + "puppetConnection.txt");

            //codeCeptConfigTemplate = codeCeptConfigTemplate.Replace("##url##", conn);
            //if (File.Exists(codeCeptConfigPath))
            //    File.Delete(codeCeptConfigPath);

            //File.WriteAllText(codeCeptConfigPath, codeCeptConfigTemplate);
            #endregion

            MyConsoleControl.WriteInput("npx codeceptjs shell", Color.AliceBlue, true);
        }

        private void toolStripButtonClearOutput_Click_1(object sender, EventArgs e)
        {
            toolStripButtonClearOutput_Click(sender, e);
        }
        #endregion ---------console

        #region CODECEPTS
        private bool _isCodeceptBusy;

        public string WriteCmd(string cmd)
        {
            string result = string.Empty;



            return result;
        }



        private async void consoleControl_OnConsoleOutput(object sender, ConsoleControl.ConsoleEventArgs args)
        {


            if (!string.IsNullOrEmpty(args.Content))
            {
                var content = args.Content.Trim().ToLower();
                if (CurrentAction != null &&
                    (content.StartsWith("fail") || content.StartsWith("error")))
                {
                    CurrentAction.IsFailed = true;
                    LastAction = CurrentAction;
                    Stop();
                }

                if (content == "i.")
                {
                    //isIReady = true;
                    MyConsoleControl.Enabled = true;                    //disable init
                    toolStripStatusLabelConsoleState.Enabled = false;

                    Console.WriteLine("Console_OUT... I.");
                    //toolStripResult.Text = args.Content;
                    //Application.DoEvents();

                    //if (IsAutoPlaying)
                    //{
                    //   CurrentOrderNo += 1;
                    //   await AutoPlay();
                    //}

                }
                else if (args.Content.Contains("Execute 'codeceptjs init' to start"))
                {
                    // MessageBox.Show("run script");
                    var param = "ReadMe.txt";
                    MyConsoleControl.WriteInput(param, Color.AliceBlue, true);

                }
                else if (args.Content.Contains("not load helper Puppeteer"))
                {
                    // MessageBox.Show("run script");
                    var param = "ReadMe.txt";
                    MyConsoleControl.WriteInput(param, Color.AliceBlue, true);

                }

                if (args.Content.Contains("Exiting interactive shell"))
                {
                    //enable init
                    toolStripStatusLabelConsoleState.Enabled = true;
                }


            }

            //xtodo confirm this behavior
            IsPlaying = false;

            toolStripStatusLabelConsoleState.Text = args.Content;

            ConsoleControl.ConsoleControl console = sender as ConsoleControl.ConsoleControl;
            console.InternalRichTextBox.ScrollToCaret();
            Application.DoEvents();



        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            var file = @"D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\Katalon\Xamun.xml";// @"D:\_MY_PROJECTS\Mond\AIFS_Manager\CodeceptSupport\Katalon\test.xml";
            //run series of commands
            Interpreter it = new Interpreter();
            it.ReadXmlFile(file);
            MyActions = it.MyActions;

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<IDataRecord, CodeceptAction>());
            //var Results = Mapper.Map<IEnumerable<CodeceptAction>, IDataReader>(MyActions);

            this.actionSource.DataSource = MyActions;

            //var json = JsonConvert.SerializeObject(aList);

        }

        #endregion


        #region FILE SYSTEM
        public void SetProjectFolder(string pathFolder)
        {

            folderBrowserDialog1.SelectedPath = pathFolder;
            saveFileDialog1.InitialDirectory = pathFolder;
            openFileDialog2.InitialDirectory = pathFolder;
        }

        //FILE SYSTEM        
        public string ProjectFolder
        {
            get
            {

                if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
                {
                    // folderBrowserDialog1.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
                    folderBrowserDialog1.SelectedPath = FileEndPointManager.DefaultXAMLFolder;

                }

                return folderBrowserDialog1.SelectedPath;
            }
            //set { folderBrowserDialog1.SelectedPath = value; }
        }





        [Obsolete]
        public async Task<CmdParam> DoCmd(CmdParam command)
        {
            var cmd = (CodeceptCmdParam)command;
            saveFileDialog1.FileName = cmd.JSFullPath;
            openFileDialog1.FileName = cmd.JSFullPath;
            //toolStripButtonSave_Click(this, EventArgs.Empty);
            //saveFileDialog1.FileName = openFileDialog1.FileName;

            //step# 8.3 it.ReadJsonTestFile(json);
            Interpreter it = new Interpreter();
            it.ReadJsonTestFile(openFileDialog1.FileName);

            //var myScript = JsonConvert.DeserializeObject<List<CodeceptAction>>(json);

            actionSource.DataSource = it.MyActions;
            refreshList();
            this.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

            IsAutoplayDone = EnumTaskStatus.Started;
            actionSource.MoveFirst();

            // ToolStripRunCodecept_Click(this, EventArgs.Empty);
            ToolStripButtonNewProcess_ClickAsync(this, EventArgs.Empty);

            //TaskWaiter.Conditions cond = new TaskWaiter.Conditions("IsAutoplayDone");
            //step# 12.3 EnumTaskStatus.DoneCodeCept
            // await cond.WaitUntil(() => IsAutoplayDone == EnumTaskStatus.DoneCodeCept);



            //wait
            ConfigManager config = new ConfigManager();
            var endPointFolder = FileEndPointManager.Project2EndPointFolder;
            var resultTxt = Path.Combine(endPointFolder, "result.txt");

            var cond = new TaskWaiter.Conditions("wait_for_result.txt");
            await cond.WaitUntil(() => AutoPlayPolicy.AssertWFOutputExist() == true, 1000).ContinueWith(x =>
              {
                  setStatus(string.Format("Retried {0} times", MyRetry), EnumPlayStatus.Success);
                  //Task.Delay(1000);

                  IsAutoPlaying = false;
                  Stop();

                  //var result =  await AutoPlay();
                  Console.WriteLine("Done.. AutoPlay");

                  //step# 12 done EnumTaskStatus.DoneCodeCept
                  IsAutoplayDone = EnumTaskStatus.DoneCodeCept;


                  command.IsSuccess = true;
                  command.IsRespond = true;

                  //step# 12.4 finished status
                  IsAutoplayDone = EnumTaskStatus.Finished;


              });




            return command;

        }


        private void toolStripLabelOpen_Click1(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = ProjectFolder;
            //openFileDialog1.Title = "Browse Script Files (json)";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                string ext = Path.GetExtension(file);

                if (ext.ToLower() == ".xml")
                {
                    ReadXML(file);
                }
                else
                {
                    saveFileDialog1.FileName = openFileDialog1.FileName;
                    //step# 8.3 it.ReadJsonTestFile(json);
                    Interpreter it = new Interpreter();
                    it.ReadJsonTestFile(openFileDialog1.FileName);

                    //var myScript = JsonConvert.DeserializeObject<List<CodeceptAction>>(json);

                    actionSource.DataSource = it.MyActions;
                    refreshList();
                    this.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                }



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

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //textBox1.Text = saveFileDialog1.FileName;
                toolStripButtonSave_Click(sender, e);

                SetProjectFolder(System.IO.Path.GetDirectoryName(saveFileDialog1.FileName));
            }
        }
        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {

            if (Path.GetExtension(saveFileDialog1.FileName.ToLower()) == ".xml")
            {
                toolStripLabelSaveAs_Click(sender, e);
            }
            else
            {
                //save
                var list = actionSource.List;
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
                //ConfigManager config = new ConfigManager();
                var codeCeptConfigPath = FileEndPointManager.MyCodeceptTestTemplate; //config.GetValue("CodeceptTestTemplate");
                var codeCeptTestTemplate = File.ReadAllText(codeCeptConfigPath);
                codeCeptTestTemplate = codeCeptTestTemplate.Replace("##steps##", codes);

                if (File.Exists(saveFileDialog1.FileName))
                    File.Delete(saveFileDialog1.FileName);

                //step# _8.5 save scenario file
                File.WriteAllText(saveFileDialog1.FileName, codeCeptTestTemplate);
                this.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            }


        }

        #endregion


        #region PLAYER


        [Obsolete]
        public bool IsAutoPlaying { get; set; }
        public bool IsPlaying { get; set; }

        public bool IsAutoRun { get; set; }


        public CodeceptAction CurrentAction
        {
            get
            {
                if (actionSource.Count > 0)
                {
                    if (actionSource.Current == null)
                        actionSource.MoveFirst();

                    return (CodeceptAction)actionSource.Current;
                }
                else
                    return null;
            }

        }

        int CurrentOrderNo
        {
            get
            {
                if (CurrentAction == null)
                    return -1;
                else
                    return CurrentAction.OrderNo;
            }
            set
            {
                if (value > -1)
                {
                    actionSource.Position = value;
                    dgActions.Rows[value].Selected = true;
                    Application.DoEvents();
                }

            }
        }

        public CodeceptAction LastAction { get; set; }
        public List<CodeceptAction> MyActions { get; set; }

        public bool IsArmReady
        {
            get { return IsCodeCeptReady(); }
            set
            {//no action 

            }
        }
        //bool isIReady;

        bool _isArmReady;

        public bool IsCodeCeptReady()
        {

            var content = toolStripStatusLabelConsoleState.Text.Trim().ToLower();

            var split = content.Split(new string[] { "\n" }, StringSplitOptions.None);

            var lastContent = split.Last().Trim();

            if (lastContent == "i." && IsPlaying == false)
                _isArmReady = true;
            else
                _isArmReady = false;

            return _isArmReady;


        }




        //step# 7.1 PlayStep() 
        public async Task<EnumPlayStatus> PlayStep()
        {

            var result = EnumPlayStatus.Playing;

            TaskWaiter.Conditions cond = new TaskWaiter.Conditions();
            //await cond.WaitUntil(() => IsCodeCeptReady() == true);

            //step# 2 await cond.WaitUntil(() => playing() == false
            IsPlaying = true;
            try
            {
                playing();

                //step# 2.wait consoleControl_OnConsoleOutput event
                await cond.WaitUntil(() => IsCodeCeptReady() == true);
                result = EnumPlayStatus.Success;
            }
            catch (Exception err)
            {
                LogApplication.Agent.LogError(err);
                result = EnumPlayStatus.Faulted;
            }

            IsPlaying = false;
            return result;

        }

        bool playing()
        {

            UpdateUIState();
            //CurrentAction = (CodeceptAction)actionSource.Current;
            if (validatePlay() == false)
            {
                return false;
            }

            //step# 7.5 play steps in codecept
            //wait for callback
            BotStepCmd stepCmd = new BotStepCmd()
            {
                Script = CurrentAction.Script
            };

            //send message to get ready and set flags for this task
            // await  BotHttpClient.TaskPostToChrome(stepCmd, "ready");       

            //step# 7.5 execute() I.step..
            MyConsoleControl.WriteInput(CurrentAction.Script //+ Environment.NewLine
                , Color.AliceBlue, true);

            //step#2 playing status
            toolStripStatusLabelConsoleState.Text = "playing";

            Console.Write("WriteInput...CurrentAction.Script... set status ... playing");

            //wait consoleControl_OnConsoleOutput event
            //TaskWaiter.Conditions cond = new TaskWaiter.Conditions();
            //await cond.WaitUntil(() => IsCodeCeptReady() == true);


            return false;//done playing
        }

        private async void dgActions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public async Task<bool> CellPlay(int RowIndex)
        {
            //dgActions.Rows[RowIndex].Selected = true;
            CurrentOrderNo = RowIndex;


            //LastAction = new CodeceptAction { IsFailed = true };
            await PlayStep();

            return true;
        }




        bool validatePlay()
        {
            //if (IsAutoPlaying)
            //    currentOrder += 1;
            if (CurrentOrderNo >= actionSource.Count)
            {
                CurrentOrderNo = actionSource.Count - 1;
                Stop();
                return false;
            }

            //if (CurrentAction.OrderNo + 1 > currentOrder)
            //{
            //    currentOrder = 0;
            //    Stop();
            //    return false;
            //}

            bool result = true;
            if (string.IsNullOrEmpty(CurrentAction.Script))
                result = false;

            return result;

        }

        public void Stop()
        {
            IsAutoPlaying = false;
            UpdateUIState();

            //actionSource.MoveFirst();
            LastAction = (CodeceptAction)actionSource.Current;

            Console.WriteLine("Stopped at Order# " + CurrentOrderNo.ToString());




        }


        bool IsLastActionFailed()
        {
            bool result = false;
            if (LastAction != null)
                result = LastAction.IsFailed;

            return result;
        }




        private void toolStripButtonStopProcess_Click_1(object sender, EventArgs e)
        {
            //stop player

            Stop();
        }


        void refreshList()
        {
            var list = (List<CodeceptAction>)actionSource.DataSource;//..List;
            var length = list.Count;

            List<CodeceptAction> newList = new List<CodeceptAction>();

            for (int i = 0; i < length; i++)
            {
                var action = list[i];
                action.OrderNo = i;
                newList.Add(action);
            }

            actionSource.DataSource = newList;
        }


        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //UP
            CurrentOrderNo = ((CodeceptAction)actionSource.Current).OrderNo;


            int index = CurrentAction.OrderNo - 1;

            if (index <= 0)
                index = 0;

            var oPrev = (CodeceptAction)actionSource.List[index];
            var current = (CodeceptAction)CurrentAction;

            actionSource.List[index] = current;
            actionSource.List[CurrentAction.OrderNo] = oPrev;

            refreshList();

            actionSource.Position = index;
            dgActions.Rows[index].Selected = true;




        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //down
            CurrentOrderNo = ((CodeceptAction)actionSource.Current).OrderNo;


            int index = CurrentAction.OrderNo + 1;
            var lenght = actionSource.List.Count - 1;

            if (index >= lenght)
                index = lenght;

            var oNext = (CodeceptAction)actionSource.List[index];
            var current = (CodeceptAction)CurrentAction;

            actionSource.List[index] = current;
            actionSource.List[CurrentAction.OrderNo] = oNext;

            refreshList();

            actionSource.Position = index;
            dgActions.Rows[index].Selected = true;


        }


        private void dgActions_MouseDown(object sender, MouseEventArgs e)
        {

        }


        #endregion -----------PLAYER

        private void ToolStripButtonPrint_Click(object sender, EventArgs e)
        {
            // ChromePartner.
            //todo compunicate to chrome to do screenshot...


        }

        private void TsButtonCodeCeptRunStep_Click(object sender, EventArgs e)
        {
            createCodeceptRunAll();
        }

        DevNoteCmd.frmDevNoteCmd createCodeceptRun()
        {
            //step# 9 if error do codecept run
            //9.take the step
            //9.1 save to test file
            //9.2 run npx
            //9.3 if success skip the step and proceed player 
            //9.4 if not success log/ stop throw error

            if (actionSource.Count == 0)
                return null;

            #region 9.1 write to template file the step
            //save
            var list = actionSource.List;
            var length = list.Count;
            //List<CodeceptAction> myList = new List<CodeceptAction>();

            string codes = string.Empty;

            CodeceptAction a = CurrentAction;
            //myList.Add(a);
            if (string.IsNullOrEmpty(a.Script))
                return null;

            var code = a.Script.Trim();

            while (code.Last() == ';')
            {
                code = code.Substring(0, code.Length - 1);
            }

            if (code.StartsWith("say('step#"))
                codes = codes + string.Format("I.{0};\n", code);
            else
                codes = codes + string.Format("I.say('step#{0}');I.{1};\n", a.OrderNo.ToString(), code);

            //step# _8.4 config.GetValue("CodeceptTestTemplate");
            ConfigManager config = new ConfigManager();
            var codeCeptConfigPath = FileEndPointManager.MyCodeceptTestTemplate;// config.GetValue("CodeceptTestTemplate");

            var codeCeptTestTemplate = File.ReadAllText(codeCeptConfigPath);
            codeCeptTestTemplate = codeCeptTestTemplate.Replace("##steps##", codes);



            //step# _8.5 dont  save this to scenario file
            //if (File.Exists(saveFileDialog1.FileName))
            //    File.Delete(saveFileDialog1.FileName);

            //File.WriteAllText(saveFileDialog1.FileName, codeCeptTestTemplate);
            //this.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);

            #endregion-----save to file

            #region 5.2 run npx
            DevNoteCmd.frmDevNoteCmd newArm = new DevNoteCmd.frmDevNoteCmd(this.Token, EnumRobotParts.CodeCeptStepArm.ToString());
            newArm.Show();

            //ConfigManager config = new ConfigManager();
            bool isCefSharp = Convert.ToBoolean(config.GetValue("IsCefSharp"));

            //step# 5.8 run in project 2 
            newArm.InitCmdConsole();
            newArm.rtxtConsole.BackColor = Color.MidnightBlue;

            //step# 5.9 run step 
            //CodeceptTestRunStep(codeCeptTestTemplate, newArm, isCefSharp);
            CodeceptTestRunRetry(codeCeptTestTemplate, newArm, isCefSharp);

            return newArm;
            #endregion-------------end run npx

        }

        [Obsolete]
        IArmPlayer createCodeceptRunAll()
        {
            //step# 9 if error do codecept run
            //9.take the step
            //9.1 save to test file
            //9.2 run npx
            //9.3 if success skip the step and proceed player 
            //9.4 if not success log/ stop throw error

            if (actionSource.Count == 0)
                return null;

            #region 9.1 write to template file the step

            //save
            var list = actionSource.List;
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

            //step# _8.4 config.GetValue("CodeceptTestTemplate");
            ConfigManager config = new ConfigManager();
            var codeCeptConfigPath = FileEndPointManager.MyCodeceptTestTemplate;//config.GetValue("CodeceptTestTemplate");
            var codeCeptTestTemplate = File.ReadAllText(codeCeptConfigPath);
            codeCeptTestTemplate = codeCeptTestTemplate.Replace("##steps##", codes);



            //step# _8.5 dont  save this to scenario file
            //if (File.Exists(saveFileDialog1.FileName))
            //    File.Delete(saveFileDialog1.FileName);

            //File.WriteAllText(saveFileDialog1.FileName, codeCeptTestTemplate);
            //this.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);

            #endregion-----save to file

            var newArm = GlobalDef.CurrentProject.CreateCodeCeptArm();

            #region 9.2 run npx

            bool isCefSharp = Convert.ToBoolean(config.GetValue("IsCefSharp"));




            var lastArm = (DevNoteCmd.frmDevNoteCmd)GlobalDef.CurrentPlayer;
            //step# 9.2 run step 
            CodeceptTestRunRetry(codeCeptTestTemplate, lastArm, isCefSharp);

            return newArm;
            #endregion-------------end run npx

        }

        private void ToolStripContainer_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //groupBoxConsole.Visible = !groupBoxConsole.Visible;
            //test
            if (CmdExeForChrome != null)
            {
                try
                {
                    //CmdExeForChrome.Close();
                    //CmdExeForChrome.Kill();
                    var handle = CmdExeForChrome.MainWindowHandle;
                    WindowsHelper.CloseWindow((int)handle);




                }
                catch (Exception err)
                {
                    LogApplication.Agent.LogError(err);
                    // throw;
                }
            }

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // ActivateGroupBox(groupLib);
            SaveAsToolStripMenuItem1_Click(sender, e);
        }

        private void BtnRec_Click(object sender, EventArgs e)
        {

            //if (flowMain.Controls.Count > 0)
            //{
            //    flowMain.Controls[0].Dock = DockStyle.None;
            //    groupBoxLimbo.Controls.Add(flowMain.Controls[0]);

            //   // newControl.Dock = DockStyle.None;
            //}

            foreach (Control c in flowMain.Controls)
            {
                if (c.Name == "groupBoxRec")
                {
                    // flowMain.Controls.Add(groupBoxRec);
                    groupBoxLimbo.Controls.Add(c);
                }
            }

            //groupBoxRec.Visible = false;
            groupLib.Visible = false;

            // ActivateGroupBox(groupBoxRec);
            //STEP_.PLAYER #1 launch puppet with extension       
            CreateChrome();




        }


        public bool IsHeadless { get; set; }
        public string RemoteDebuggerAddress { get; set; }

        public void CreateChrome()
        {

            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            string drive = Path.GetPathRoot(dir);
            string driveLetter = drive.First().ToString();

            var param = string.Format("cd /{0} {1}\\CodeceptJs\\Project2", driveLetter, dir);


            //MyConsoleControlForChrome.WriteInput("node LaunchChromeExt.js", Color.AliceBlue, true);

            var batFolder = string.Format("{0}\\Bat", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var batPath = Path.Combine(batFolder, "RunChromeExt.bat");
            var batTemplate = File.ReadAllText(batPath);
            batTemplate = batTemplate.Replace("##Home##", param);

            //ConfigManager config = new ConfigManager();
            //var exe = config.GetValue("ChromeExe");

            var exe = FileEndPointManager.MyChromeViaRecorder;
            batTemplate = batTemplate.Replace("##.exe##", exe);


            var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeceptBatPath = Path.Combine(codeceptjsFolder, "RunChromeExt.bat");


            if (File.Exists(codeceptBatPath))
                File.Delete(codeceptBatPath);

            File.WriteAllText(codeceptBatPath, batTemplate);


            //step# 2 run bat file
            CmdExeForChrome = RunHelper.ExecuteCommandSilently(codeceptBatPath);

            return;

            #region CONFIG using files - not needed  for now

            //step# 7 codecept.conf.js
            codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeCeptConfigPath = Path.Combine(codeceptjsFolder, "codecept.conf.js");
            string codeCeptConfigTemplate;


            if (IsHeadless)
            {
                //xtodo Make this universal like step_file.js.template.js
                codeCeptConfigTemplate = File.ReadAllText(Path.Combine(codeceptjsFolder, "headless_codecept.conf.template.txt"));

            }
            else
            {
                codeCeptConfigTemplate = File.ReadAllText(Path.Combine(codeceptjsFolder, "codecept.conf.template.txt"));

            }



            codeCeptConfigTemplate = codeCeptConfigTemplate.Replace("##url##", RemoteDebuggerAddress);
            if (File.Exists(codeCeptConfigPath))
                File.Delete(codeCeptConfigPath);

            File.WriteAllText(codeCeptConfigPath, codeCeptConfigTemplate);

            //step# 5.3 set step_file.js            
            var fileTemplateFolder = string.Format("{0}\\CodeceptJs", dir);
            var fileTemplate = Path.Combine(fileTemplateFolder, "steps_file.js.template.js");
            var contentTemplate = File.ReadAllText(fileTemplate);


            //step# 5.4 create steps_file.js 
            codeCeptConfigPath = Path.Combine(codeceptjsFolder, "steps_file.js");
            if (File.Exists(codeCeptConfigPath))
                File.Delete(codeCeptConfigPath);
            File.WriteAllText(codeCeptConfigPath, contentTemplate);


            #endregion


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



        public static Process CmdExeForChrome;
        public static Process CmdExeForCodecept;


        public string InitialDirectory { get; set; }
        public string InitialChangeDirCmd { get; set; }

        private void FrmDevNoteCmd_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseChromeWindow();
            CloseCodeCeptJsWindow();

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


        public async Task<bool> Play(bool isRecording = false)
        {

            // this.InvokeOnUiThreadIfRequired(() => ShowHelper());


            CloseCodeCeptJsWindow();

            // ConfigManager config = new ConfigManager();
            var defaultXML = FileEndPointManager.DefaultPlayJsFile; // config.GetValue("DefaultXMLFile");


            if (isRecording)
            {
                var endPointFolder = FileEndPointManager.Project2Folder;

                //TODO check for error in UI
                //ActivateGroupBox(groupBoxRec);
                defaultXML = Path.Combine(endPointFolder, "latest.xml");
            }

            groupBoxRec.Visible = true;





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
            // dgActions.DataSource = this.actionSource;//.DataSource;

            //play


            dgActions.DataSource = actionSource;

            dgActions.Refresh(); // Make sure this comes first
            dgActions.Parent.Refresh(); // Make sure this comes second
            flowMain.Refresh();

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

        private async void BtnPlay_Click(object sender, EventArgs e)
        {
            // await Play();
            await Run(FileEndPointManager.DefaultPlayJsFile);

        }

        private void Open_File(string jsXMLFile)
        {
            //openFileDialog1.InitialDirectory = ProjectFolder;
            //openFileDialog1.Title = "Browse Script Files (json)";

            openFileDialog1.FileName = jsXMLFile;

            string file = openFileDialog1.FileName;
            string ext = Path.GetExtension(file);

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
                actionSource.DataSource = it.MyActions;
                refreshList();
                ////end obsolete


                this.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            }


        }

        void InsertVariables()
        {

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

            this.actionSource.DataSource = MyActions;
            refreshList();

            this.Text = "Dev Note Recorder -" + Path.GetFileName(openFileDialog1.FileName);
            //saveFileDialog1.FileName = openFileDialog1.FileName;

            // var folder = Path.GetDirectoryName(jsXMLFile);


            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            string drive = Path.GetPathRoot(dir);
            string driveLetter = drive.First().ToString();

            var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeceptTestPath = Path.Combine(codeceptjsFolder, "latest_test.js");


            if (File.Exists(codeceptTestPath))
                File.Delete(codeceptTestPath);


            saveFileDialog1.FileName = codeceptTestPath;

            //step# 30 Save to default js file 
            toolStripButtonSave_Click(this, EventArgs.Empty);


        }

        private void BtnRec_MouseHover(object sender, EventArgs e)
        {
            hideControlsExcept(panel1);
        }

        void hideControlsExcept(Control activeControl)
        {
            foreach (Control c in flowMain.Controls)
            {
                c.Visible = false;
            }

            activeControl.Visible = true;
        }


        private void BtnSave_MouseHover(object sender, EventArgs e)
        {
            hideControlsExcept(panel3);
        }

        private void BtnPlay_MouseHover(object sender, EventArgs e)
        {
            hideControlsExcept(panel2);
        }

        private void BtnRec_MouseLeave(object sender, EventArgs e)
        {
            panel1.Visible = panel2.Visible = panel3.Visible = false;
        }

        private void FrmDevNoteCmd_MouseLeave(object sender, EventArgs e)
        {
            BtnRec_MouseLeave(sender, e);
        }

        private void FlowLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            BtnRec_MouseLeave(sender, e);
        }

        private void FrmDevNoteCmd_Move(object sender, EventArgs e)
        {
            WindowsHelper.FollowConsole(this, CmdExeForCodecept);

        }

        private void FrmDevNoteCmd_Resize(object sender, EventArgs e)
        {
            WindowsHelper.FollowConsole(this, CmdExeForCodecept);
        }



        private async void OpenToolStripMenuItemOPEN_Click(object sender, EventArgs e)
        {

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                Run(openFileDialog2.FileName);
            }

        }



        private async Task<bool> AutoRun(string filepath, int maxRetry = 3)
        {
            int retry = 0;
            bool result = false;
            //STEP.Player #801 AUTORUN
            do
            {
                //STEP.Player #802 RETRY                
                result = await Run(filepath);
                retry += 1;



            } while (result == false && retry < maxRetry);

            //CloseChromeWindow();
            CloseCodeCeptJsWindow();

            string errorCode = string.Empty;
            if (result == false && retry == maxRetry)
            {
                //ErrorCodes.MaxTryLimit
                errorCode = ErrorCodes.MaxTryLimit.ToString();
            }
            else if (result == false)
            {

            }

            //await BotHttpClient.TaskHttpGetToDesigner("Done");
            //package to cmd

            //STEP_.RESULT #1
            var payLoad = FileEndPointManager.ReadInputWFCmdJsonFile();
            payLoad.IsSuccess = result;
            payLoad.RetryCount = retry;
            payLoad.ErrorCode = errorCode;
            var cmdCarrier = CmdParam.CreateCmdCarrier<RunWFCmdParam>(payLoad, EnumCmd.EndWFResult.ToString());
            cmdCarrier.Payload = payLoad;

            //rewrite inputFile to update status
            FileEndPointManager.CreateInputWF(payLoad, true);


            //STEP_.RESULT #2 send result to DevNOte           
            //await BotHttpClient.PostToDevNote("Done");
            _ = await BotHttpClient.PostToDevNote(cmdCarrier);



            this.Close();
            return true;
        }


        private async Task<bool> Run(string filepath)
        {
            //STEP.Player #850 reset results.txt
            GlobalPlayer.ResetResult();

            openFileDialog2.FileName = filepath;

            var selectedJSXML = openFileDialog2.FileName;

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

                    WindowsHelper.FollowConsole(this, CmdExeForCodecept);


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



        private void NewToolStripMenuItemNEW_Click(object sender, EventArgs e)
        {
            BtnRec_Click(sender, e);
        }

        private void SettingToolStripMenu_Click(object sender, EventArgs e)
        {
            var config = new DotBits.Configuration.ConfigFrm();
            config.ShowDialog();
        }

        private void ToolStripMenuItemTEST_Click(object sender, EventArgs e)
        {
            ConfigManager config = new ConfigManager();
            var endPointFolder = config.GetValue("DefaultXMLFile");

            endPointFolder = Path.GetDirectoryName(endPointFolder);
            var txtFile = Path.Combine(endPointFolder, "play.txt");
            //File.CreateText(txtFile);
            File.WriteAllText(txtFile, "test");
        }

        private void SaveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Path.GetExtension(saveFileDialog1.FileName.ToLower()) == ".xml")
            {
                toolStripLabelSaveAs_Click(sender, e);
            }
            else
            {
                //save
                var list = actionSource.List;
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
                //ConfigManager config = new ConfigManager();
                var codeCeptConfigPath = FileEndPointManager.MyCodeceptTestTemplate;//config.GetValue("CodeceptTestTemplate");
                var codeCeptTestTemplate = File.ReadAllText(codeCeptConfigPath);
                codeCeptTestTemplate = codeCeptTestTemplate.Replace("##steps##", codes);

                if (File.Exists(saveFileDialog1.FileName))
                    File.Delete(saveFileDialog1.FileName);

                //step# _8.5 save scenario file
                File.WriteAllText(saveFileDialog1.FileName, codeCeptTestTemplate);
                this.Text = "Dev Note Console -" + Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            }

        }

        private void SaveAsToolStripMenuItem1_Click(object sender, EventArgs e)
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

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //textBox1.Text = saveFileDialog1.FileName;
                toolStripButtonSave_Click(sender, e);

                SetProjectFolder(System.IO.Path.GetDirectoryName(saveFileDialog1.FileName));
            }
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ActivateGroupBox(groupLib);

        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        #region HEART BEAT

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //  HeartBeat();
        }




        #endregion

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            openLibrary();
        }




        void openLibrary()
        {
            //open Lib
            hideControlsExcept(panel3);
            DevNoteWindowsFormsControlLibrary.DevNoteLibForm frm = new DevNoteWindowsFormsControlLibrary.DevNoteLibForm();
            frm.PlayWithChrome += BtnPlay_Click;

            frm.Show();
            WindowsHelper.FollowConsole(this, frm.Handle);

        }

        public void InvokeOnUiThreadIfRequired(Action action)
        {
            var control = this;
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}

