using CodeceptSupport;
using DevNote.Interface;
using DevNote.Interface.Common;
using DevNoteCmdPlayer;
using LogApplication.Common;
using LogApplication.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWaiter;

namespace Player
{
    class CmdPlayer : IfrmDevNoteCmd
    {
        public int ArmId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IChromeIdentity ChromePartner { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public CodeceptAction CurrentAction
        {
            //get
            //{
            //if (actionSource.Count > 0)
            //{
            //    if (actionSource.Current == null)
            //        actionSource.MoveFirst();

            //    return (CodeceptAction)actionSource.Current;
            //}
            //else
            //    return null;
            //}
            get => throw new NotImplementedException(); set => throw new NotImplementedException();


        }


        public string InitialChangeDirCmd { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string InitialDirectory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsArmReady { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public EnumTaskStatus IsAutoplayDone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsAutoPlaying { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsAutoRun { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsHeadless { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsPlaying { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSessionLifeSpan { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string JSFile { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CodeceptAction LastAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<CodeceptAction> MyActions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MyRetry { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string ProjectFolder => throw new NotImplementedException();

        public string RemoteDebuggerAddress { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public EnumPlayStatus Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public CmdToken Token { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Task<bool> CellPlay(int RowIndex)
        {
            throw new NotImplementedException();
        }


        public void ConnectToChrome(IChromeIdentity chrome)
        {
            throw new NotImplementedException();
        }

        public void CreateChrome()
        {
            throw new NotImplementedException();
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
    }
}
