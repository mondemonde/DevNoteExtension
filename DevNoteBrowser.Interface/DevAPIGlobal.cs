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
    public static class DevAPI
    {

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
                //int RevisionNumber = (int)(DateTime.UtcNow - new DateTime(2020, 1, 1)).Seconds;
                return exe;
            }

        }

        public static Version GetVersion()
        {
            return System.Reflection.Assembly.GetAssembly(typeof(DevAPI)).GetName().Version;

        }
    }
}
