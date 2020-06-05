﻿using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Player
{
    class Program
    {
        // public static int RemoteDebuggerPort = 8088;
        static bool IsAuto { get; set; }
        static bool IsHeadless { get; set; }
        static string JSFile { get; set; }
        //public static RunWFCmdParam MyPayload { get; set; }
        //static WinPlayer MyPlayer = new WinPlayer();
        //public static System.IO.FileSystemWatcher fileWatcher;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static  void Main(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-file")
                {
                    // call http client args[i+1] for URL
                    JSFile = args[i + 1];

                }
                else if (args[i] == "-isHeadless")
                {
                    // call http client args[i+1] for URL
                    IsHeadless = Convert.ToBoolean(args[i + 1]);
                }
                else if (args[i] == "-isAutoRun")
                {
                    // call http client args[i+1] for URL
                    IsAuto = Convert.ToBoolean(args[i + 1]);
                }
            }
            var application = new App();
            application.InitializeComponent();
            application.Run();  
        }
    }
}
