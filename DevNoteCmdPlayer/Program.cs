using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevNoteCmdPlayer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmDevNoteCmd());
            //Application.Run(new FormCmd());
            var cmd = "new";
            while (cmd!="exit")
            {
                if (cmd == "new")
                {
                    Application.Run(new frmDevNoteCmd());
                    Console.WriteLine("type new or exit.");
                }
                else                    
                    Console.WriteLine("type new or exit.");

                cmd = Console.ReadLine();
            }


        }
    }
}
