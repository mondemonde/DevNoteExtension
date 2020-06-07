using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

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


            string baseAddress = "http://localhost:9876/";
            var config = new HttpSelfHostConfiguration(baseAddress);

            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();

                Console.WriteLine("API app started.");
                Console.WriteLine(String.Format("Listening on: {0}", baseAddress));

                var application = new App();
                application.InitializeComponent();
                application.Run();
            }


          



        }
    }
}
