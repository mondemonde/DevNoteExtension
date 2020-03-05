using LogApplication.Common.Commands;
using System;

namespace DevNote.Interface
{
    public interface IDevNoteBrowser
    {

        IChromeIdentity Identity { get; set; }

        CmdParam DoCmd(CmdParam param);
        bool IsPageReady { get; set; }

        IFrontWF FrontWF { get; set; }
        void InitializeChrome(int remoteDebuggerPort = 8088);

        bool IsMainWFReady { get; set; }
        DateTime RequestDateToken { get; set; }
        string FilePathSourceDownload { get; set; }

        

        #region OBSOLETE

        bool IsClickDone { get; set; }
        bool IsScrollDone { get; set; }
        bool IsGoToDone { get; set; }
        bool IsSaveToFileDone { get; set; }
        bool IsTypeKeyDone { get; set; }
        bool IsConsoleTestDone { get; set; }
        bool IsFindDone { get; set; }
        bool IsCodeceptDone { get; set; }
        bool IsPuppetDone { get; set; }
        bool IsDebugDone { get; set; }
        bool IsEmailDone { get; set; }

       

        string CurrentPageUrl { get; set; }

        bool IsBrowserInitialized { get; set; }
        bool IsDebugMode { get; set; }
        bool IsInterceptorRunning { get; set; }

        JSPayload SelectedPoint { get; set; }

        event EventHandler<EventArgs> GetTextToFindEvent;

        void Clickme(int x, int y, string textToFind, bool clickTAB = false);
        void DisplayOutput(string output);
        void DoScreenShot(string folder, string fileName);
        string GetContent();
        void JustClickMe(int x, int y);
        void Key_byKeyCode(int VK);
        void Key_TAB();
        void LoadPage(string url);
        void LoadUrl(string url, string textToFind);
        void MouseScroll(int x, int y, int deltaX, int deltaY, string textToFind);
        void SaveToFile();
        void SaveToFile(string fileFullPath);
        void ScrollKey(int x, int y, string textToFind, string direction = "Down", int repeat = 1);
        void TypeKey(string text, int x, int y, string textToFind, bool autoTAB = true);

        #endregion
       
        //void Show();
    }

   
}