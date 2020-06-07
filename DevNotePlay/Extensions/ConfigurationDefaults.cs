using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Extensions
{
    public static class ConfigurationDefaults
    {
        #region Endpoints
        public static string DevNoteFrontUrl
        {
            get
            {
                return "DevNoteFrontUrl";
            }
        }
        public static string Project2Folder
        {
            get
            {
                return "Project2Folder";
            }
        }
        public static string TenantId
        {
            get
            {
                return "TenantId";
            }
        }
        public static string LogFile
        {
            get
            {
                return "LogFile";
            }
        }
        public static string MaxTimeOutMinutes
        {
            get
            {
                return "MaxTimeOutMinutes";
            }
        }
        #endregion

        #region Main Folders
        public static string MyMainFolder
        {
            get
            {
                return "MyMainFolder";
            }
        }
        public static string CommonExeFolder
        {
            get
            {
                return "CommonExeFolder";
            }
        }
        public static string ChromeExe
        {
            get
            {
                return "ChromeExe";
            }
        }
        public static string DevNoteAPIExe
        {
            get
            {
                return "DevNoteAPIExe";
            }
        }
        #region Advanced Settings
        public static string AzureServiceBusReceiver
        {
            get
            {
                return "AzureServiceBusReceiver";
            }
        }
        public static string AzureServiceBusSender
        {
            get
            {
                return "AzureServiceBusSender";
            }
        }
        public static string ChromeRemoteDebuggerFile_Window
        {
            get
            {
                return "ChromeRemoteDebuggerFile_Window";
            }
        }
        public static string ChromeRemoteDebuggerFile_Headless
        {
            get
            {
                return "ChromeRemoteDebuggerFile_Headless";
            }
        }
        public static string ScriptToolBox
        {
            get
            {
                return "ScriptToolBox";
            }
        }
        public static bool IsCefSharp
        {
            get
            {
                return false;
            }
        }
        public static bool IsHeadless
        {
            get
            {
                return false;
            }
        }
        #endregion
        #endregion

        #region Recordings
        public static string DevNotePlayerExe
        {
            get
            {
                return "DevNotePlayerExe";
            }
        }
        public static string Click_Default_Wait
        {
            get
            {
                return "Click_Default_Wait";
            }
        }
        public static string ScreenshotOnFail
        {
            get
            {
                return "ScreenshotOnFail";
            }
        }
        public static string TestKey
        {
            get
            {
                return "TestKey";
            }
        }
        #endregion

        #region Defaults Event Entry
        public static string Default_Domain
        {
            get
            {
                return "Default_Domain";
            }
        }
        public static string Default_Dept
        {
            get
            {
                return "Default_Dept";
            }
        }
        public static string Default_EventTag
        {
            get
            {
                return "Default_EventTag";
            }
        }
        #endregion

        #region Designer
        public static string DevNoteDesignerExe
        {
            get
            {
                return "DevNoteDesignerExe";
            }
        }
        public static string DevNoteDesignerLibrary
        {
            get
            {
                return "DevNoteDesignerLibrary";
            }
        }
        #endregion
    }
}
