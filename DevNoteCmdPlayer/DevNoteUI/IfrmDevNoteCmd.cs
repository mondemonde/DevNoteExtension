using CodeceptSupport;
using DevNote.Interface;
using DevNote.Interface.Common;
using LogApplication.Common;
using LogApplication.Common.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaskWaiter;

namespace DevNoteCmdPlayer
{
    public interface IFrmDevNoteCmd
    {

        void InvokeOnUiThreadIfRequired(Action action);

        int ArmId { get; set; }
        IChromeIdentity ChromePartner { get; set; }
        CodeceptAction CurrentAction { get; }
        string InitialChangeDirCmd { get; set; }
        string InitialDirectory { get; set; }
        bool IsArmReady { get; set; }
        EnumTaskStatus IsAutoplayDone { get; set; }
        bool IsAutoPlaying { get; set; }
        bool IsAutoRun { get; set; }
        bool IsHeadless { get; set; }
        bool IsPlaying { get; set; }
        bool IsSessionLifeSpan { get; set; }
        string JSFile { get; set; }
        CodeceptAction LastAction { get; set; }
        List<CodeceptAction> MyActions { get; set; }
        int MyRetry { get; set; }
        string ProjectFolder { get; }
        string RemoteDebuggerAddress { get; set; }
        EnumPlayStatus Status { get; set; }
        CmdToken Token { get; set; }

        Task<bool> CellPlay(int RowIndex);

        //void CodeceptTestRun(string FilePath, DevNoteCmd.frmDevNoteCmd rightArm, bool isCefSharp = true);
        //void CodeceptTestRunRetry(string codeCeptTestFileContent, DevNoteCmd.frmDevNoteCmd rightArm, bool isCefSharp = true);

        void ConnectToChrome(IChromeIdentity chrome);
        void CreateChrome();
        Task<CmdParam> DoCmd(CmdParam command);
        bool IsCodeCeptReady();
        Task<bool> Play(bool isRecording = false);
        Task<EnumPlayStatus> PlayStep();
        Task<IArmPlayer> Retry();
        void RunCondeceptjsDefault();
        void SetProjectFolder(string pathFolder);
        void Stop();
        void TestRunAsync(CodeceptAction action);
        string WriteCmd(string cmd);
    }
}