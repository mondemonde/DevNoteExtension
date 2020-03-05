using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DevNoteCmdPlayer2.Helpers
{
    public static class WindowsHelper
    {

        #region GENERAL

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_CLOSE = 0xF060;

        public static void CloseWindow(int iHandle)
        {
            // retrieve the handler of the window  
            //int iHandle = FindWindow("Notepad", "Untitled - Notepad");
            if (iHandle > 0)
            {
                // close the window using API        
                SendMessage(iHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
            }
        }
        #endregion


        #region -----------------------------CONSOLE------------------
        const int SWP_NOZORDER = 0x4;
        const int SWP_NOACTIVATE = 0x10;

        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();


        [DllImport("user32")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int x, int y, int cx, int cy, int flags);

        /// <summary>
        /// Sets the console window location and size in pixels
        /// </summary>
        public static void SetWindowPosition(IntPtr handle, int x, int y, int width, int height)
        {
            SetWindowPos(handle, IntPtr.Zero, x, y, width, height, SWP_NOZORDER | SWP_NOACTIVATE);
        }

        //public static IntPtr HandleWnd
        //{
        //    get
        //    {
        //        //Initialize();
        //        return GetConsoleWindow();
        //    }
        //}

        //private void Form1_Move(object sender, EventArgs e)
        //{
        //    FollowConsole();
        //}

        public static void FollowConsole(Form frm,Process exe)
        {
            try
            {

                if (exe != null)
                {

                    IntPtr handle = exe.MainWindowHandle;

                    var screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                    var width = screen.Width;
                    var height = screen.Height;

                   //SetWindowPosition(handle, frm.Left , frm.Top + frm.Height, 800, 900);
                   SetWindowPosition(handle, frm.Left , frm.Top + frm.Height, 800, 900);


                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
               // throw;
            }
          
            //Console.WriteLine("WARNING! Closing this console will terminate the Geo Addressing tool application.");

        }

        public static void FollowConsole(Process exe)
        {
            try
            {

                if (exe != null)
                {

                    IntPtr handle = exe.MainWindowHandle;

                    var screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                    var sWidth = screen.Width;
                    var sHeight = screen.Height;
                    int posX, posY;
                    posX = sWidth - 800;
                    posY = 170;

                    //SetWindowPosition(handle, frm.Left , frm.Top + frm.Height, 800, 900);
                    SetWindowPosition(handle, posX , posY, 800, 600);


                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
               // throw;
            }
          
            //Console.WriteLine("WARNING! Closing this console will terminate the Geo Addressing tool application.");

        }


        public static void FollowConsole( double windowWidth, double windowHeight)
        {
            try
            {
                Process exe = Process.GetCurrentProcess();
                
                if (exe != null)
                {

                    IntPtr handle = exe.MainWindowHandle;

                    var screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                    var sWidth = screen.Width;
                    var sHeight = screen.Height;
                    int posX, posY;
                    posX = sWidth - 790;
                    posY = 50;
                    //SetWindowPosition(handle, frm.Left , frm.Top + frm.Height, 800, 900);
                    SetWindowPosition(handle, posX, posY,Convert.ToInt32(windowWidth),Convert.ToInt32(windowHeight));


                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                // throw;
            }

            //Console.WriteLine("WARNING! Closing this console will terminate the Geo Addressing tool application.");

        }


        public static void FollowConsole(Form frm, IntPtr handle)
        {
            try
            {

               

                    var screen = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
                    var width = screen.Width;
                    var height = screen.Height;

                    SetWindowPosition(handle, frm.Left, frm.Top + frm.Height, 800, 640);

               
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                // throw;
            }

            //Console.WriteLine("WARNING! Closing this console will terminate the Geo Addressing tool application.");

        }




        #endregion




    }
}
