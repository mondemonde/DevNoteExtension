using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    class Program
    {

        // public static int RemoteDebuggerPort = 8088;
        static bool IsAuto { get; set; }
        static bool IsHeadless { get; set; }

        //public static RunWFCmdParam MyPayload { get; set; }
       // static CmdPlayer MyPlayer = new CmdPlayer();


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
                  //  MyPlayer.JSFile = args[i + 1];

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



            Console.WriteLine("Hello World!");
            var application = new App();
            application.InitializeComponent();
            application.Run();


          
        }
    }
}
