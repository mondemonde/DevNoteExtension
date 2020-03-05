using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevNoteCmdPlayer;
using DevNote.Interface;
using CodeceptSupport;
using TaskWaiter;
using DevNote.Interface.Common;
using LogApplication.Common;
using LogApplication.Common.Commands;
using System.IO;
using Common;
using System.Diagnostics;

namespace DevNoteCmdPlayer2.DevNoteUI
{
    public partial class UserControlPlayer : UserControl, IFrmDevNoteCmd
    {
        public UserControlPlayer()
        {
            InitializeComponent();
        }

        public int ArmId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IChromeIdentity ChromePartner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CodeceptAction CurrentAction { get; set; }

        public string InitialChangeDirCmd { get; set; }
        public string InitialDirectory { get; set; }
        public bool IsArmReady { get; set; }
        public EnumTaskStatus IsAutoplayDone { get; set; }
        public bool IsAutoPlaying { get; set; }
        public bool IsAutoRun { get; set; }
        public bool IsHeadless { get; set; }
        public bool IsPlaying { get; set; }
        public bool IsSessionLifeSpan { get; set; }
        public string JSFile { get; set; }
        public CodeceptAction LastAction { get; set; }
        public List<CodeceptAction> MyActions { get; set; }
        public int MyRetry { get; set; }

        public string ProjectFolder { get; set; }

        public string RemoteDebuggerAddress { get; set; }
        public EnumPlayStatus Status { get; set; }
        public CmdToken Token { get; set; }

        public Task<bool> CellPlay(int RowIndex)
        {
            throw new NotImplementedException();
        }

        public void ConnectToChrome(IChromeIdentity chrome)
        {
            throw new NotImplementedException();
        }

        public static Process CmdExeForChrome;
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

            var exe = DevAPI.MyChromeViaRecorder;
            batTemplate = batTemplate.Replace("##.exe##", exe);


            var codeceptjsFolder = string.Format("{0}\\CodeceptJs\\Project2", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var codeceptBatPath = Path.Combine(codeceptjsFolder, "RunChromeExt.bat");


            if (File.Exists(codeceptBatPath))
                File.Delete(codeceptBatPath);

            File.WriteAllText(codeceptBatPath, batTemplate);


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

        public Task<bool> Play(bool isRecording = false)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        public void InvokeOnUiThreadIfRequired( Action action)
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
