using DevNote.Interface;
using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
   public static class RunHelper
    {
        /// <summary>
        /// batfilename with .bat
        /// </summary>
        /// <param name="BatFileName"></param>
        public static Process RunBatFile(string BatFileName, Dictionary<string, string> replaceList =null,bool IsBlocking = false)
        {

            var dir = LogApplication.Agent.GetCurrentDir();
            dir = dir.Replace("file:\\", string.Empty);
            //string drive = Path.GetPathRoot(dir);
            //string driveLetter = drive.First().ToString();
           // var param = string.Format("cd /{0} {1}\\Bat\\exe", driveLetter, dir);



            var batFolder = string.Format("{0}\\Bat", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var batPath = Path.Combine(batFolder, BatFileName);
            var batTemplate = File.ReadAllText(batPath);



          //  batTemplate = batTemplate.Replace("##Home##", param);


            if (replaceList != null)
            {
                foreach (var pair in replaceList)
                {

                    batTemplate = batTemplate.Replace(pair.Key, pair.Value);

                }
            }
            var dirBatFolder = string.Format("{0}\\Bat\\exe", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
            var dirBatPath = Path.Combine(dirBatFolder, BatFileName);

            try
            {
                if (File.Exists(dirBatPath))
                    File.Delete(dirBatPath);

                File.WriteAllText(dirBatPath, batTemplate);
            }
            catch (Exception err)
            {
                BotHttpClient.Log(err.Message, true);
               // throw;
            }

           

            //step# 2 run bat file
            if (IsBlocking)
            {
                return null;
            }
            else
                {

                    return ExecuteCommandSilently(dirBatPath);
                }

        }

        //public static Process RunBatFile(string BatFileName ,string argument, Dictionary<string, string> replaceList = null, bool IsBlocking = false)
        //{

        //    var dir = LogApplication.Agent.GetCurrentDir();
        //    dir = dir.Replace("file:\\", string.Empty);
        //    //string drive = Path.GetPathRoot(dir);
        //    //string driveLetter = drive.First().ToString();
        //    // var param = string.Format("cd /{0} {1}\\Bat\\exe", driveLetter, dir);



        //    var batFolder = string.Format("{0}\\Bat", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
        //    var batPath = Path.Combine(batFolder, BatFileName);
        //    var batTemplate = File.ReadAllText(batPath);



        //    //  batTemplate = batTemplate.Replace("##Home##", param);


        //    if (replaceList != null)
        //    {
        //        foreach (var pair in replaceList)
        //        {

        //            batTemplate = batTemplate.Replace(pair.Key, pair.Value);

        //        }
        //    }
        //    var dirBatFolder = string.Format("{0}\\Bat\\exe", dir);  //@"D:\_ROBOtFRAMeWORK\CodeceptsJs\Project1\";
        //    var dirBatPath = Path.Combine(dirBatFolder, BatFileName);


        //    if (File.Exists(dirBatPath))
        //        File.Delete(dirBatPath);

        //    File.WriteAllText(dirBatPath, batTemplate);

        //    //step# 2 run bat file
        //    if (IsBlocking)
        //    {
        //        return null;
        //    }
        //    else
        //    {

        //        return ExecuteCommandSilently(dirBatPath);
        //    }

        //}


        public static void RunConsoleCmd(string arguments = "")
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            // cmd.StartInfo.RedirectStandardInput = true;
            //cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Arguments = arguments;
            cmd.Start();

            //cmd.StandardInput.WriteLine("echo Oscar");
            // cmd.StandardInput.Flush();
            //  cmd.StandardInput.Close();
            // cmd.WaitForExit();-
            //   Console.WriteLine(cmd.StandardOutput.ReadToEnd());
        }

        public static Process ExecuteCommand(string command)
        {

            var exePath = "C:\\Windows\\System32\\cmd.exe";

            var processInfo = new ProcessStartInfo(exePath, "/K " + command);
            // processInfo.CreateNoWindow = true;

            // processInfo.UseShellExecute = false;

            //processInfo.RedirectStandardError = true;
            // processInfo.RedirectStandardOutput = true;
            //processInfo.WindowStyle = ProcessWindowStyle.Minimized;

            var process = Process.Start(processInfo);

            // process.WaitForExit();

            return process;

            //process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            //    Console.WriteLine("output>>" + e.Data);
            //process.BeginOutputReadLine();

            //process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            //    Console.WriteLine("error>>" + e.Data);
            //process.BeginErrorReadLine();



            //Console.WriteLine("ExitCode: {0}", process.ExitCode);
            //process.Close();
        }
        public static Process ExecuteCommandSilently(string command)
        {

            var exePath = "C:\\Windows\\System32\\cmd.exe";

            var processInfo = new ProcessStartInfo(exePath, "/c " + command);
            //processInfo.CreateNoWindow = true;

            // processInfo.UseShellExecute = false;

            //processInfo.RedirectStandardError = true;
            // processInfo.RedirectStandardOutput = true;
            processInfo.WindowStyle = ProcessWindowStyle.Minimized;

            var process = Process.Start(processInfo);

            // process.WaitForExit();

            return process;

            //process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            //    Console.WriteLine("output>>" + e.Data);
            //process.BeginOutputReadLine();

            //process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            //    Console.WriteLine("error>>" + e.Data);
            //process.BeginErrorReadLine();



            //Console.WriteLine("ExitCode: {0}", process.ExitCode);
            //process.Close();
        }


        public static void ExecuteWaitCommand(string command, string workingFolder,EnumProcess proc,  bool IsCreateNoWindow=false)
        {
            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            ProcessInfo.CreateNoWindow = IsCreateNoWindow;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.WorkingDirectory = workingFolder;

            // *** Redirect the output ***

          //  ProcessInfo.RedirectStandardError = true;
          //  ProcessInfo.RedirectStandardOutput = true;

            ProcessInfo.RedirectStandardError = false;
            ProcessInfo.RedirectStandardOutput = false;

            process = Process.Start(ProcessInfo);

            switch (proc)
            {
                case EnumProcess.NonGlobal:
                    break;
                case EnumProcess.Designer:
                    GlobalDef.CurrentDesigner.Process = process;
                    break;
                case EnumProcess.Player:
                    GlobalDef.ProcessPlayer = process;
                    break;
                case EnumProcess.Receiver:
                    GlobalDef.ProcessReceiver = process;
                    break;
                case EnumProcess.Sender:
                    GlobalDef.ProcessSender = process;
                    break;
                default:
                    break;
            }


            process.WaitForExit();

            // *** Read the streams ***
          //  string output = process.StandardOutput.ReadToEnd();
          //  string error = process.StandardError.ReadToEnd();

            ExitCode = process.ExitCode;

            if(ExitCode>0)
            {
                BotHttpClient.Log("ERROR on " + proc.ToString(), true);
            }

           // MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
           // MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
          //  MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");
            process.Close();

            process = null;

            switch (proc)
            {
                case EnumProcess.NonGlobal:
                    break;
                case EnumProcess.Designer:
                    GlobalDef.CurrentDesigner.Process = process;
                    break;
                case EnumProcess.Player:
                    GlobalDef.ProcessPlayer = process;
                    break;
                case EnumProcess.Receiver:
                    GlobalDef.ProcessReceiver = process;
                    break;
                case EnumProcess.Sender:
                    GlobalDef.ProcessSender = process;
                    break;
                default:
                    break;
            }


            #region EXAMPLE
            //// This will get the current WORKING directory (i.e. \bin\Debug)
            //string workingDirectory = Environment.CurrentDirectory;
            //// This will get the current PROJECT directory
            //string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
            //string commandToExecute = Path.Combine(projectDirectory, "TestSetup", "WreckersTestSetupQA.bat");
            //string workingFolder = Path.GetDirectoryName(commandToExecute);
            //commandToExecute = QuotesAround(commandToExecute);
            //ExecuteCommand(commandToExecute, workingFolder);

            #endregion

        }


        public static void AutoRunPlayer(string jsFile)
        {
            ConfigManager config = new ConfigManager();
            //    <add key="DevNoteDesignerLibrary" value="D:\_MY_PROJECTS\_DEVNOTE\_DevNote3\DevNoteDesignerLibrary" />
            var exe = config.GetValue("DevNotePlayerExe");
            var dir = System.IO.Path.GetDirectoryName(exe);


            string drive = System.IO.Path.GetPathRoot(dir);
            string driveLetter = drive.First().ToString();

            var param = string.Format("cd /{0} {1}\\Bat\\exe", driveLetter, dir);

            var newValues = new Dictionary<string, string>
            {
                { "##Home##", param},
                { "##.exe##", exe + " -isAutoRun true -file " + jsFile } //with parameter
            };


            //var newValues = new Dictionary<string, string>
            //{
            //    //{ "username", userName },
            //    { "##.exe##", exe }
            //};
            RunBatFile(EnumFiles.Player, newValues);

            //MainLogic.PrependMesage("Player/Recorder started...");
            //MainWindow.MyDisplayState.UpdateView();

        }


        public static void MinimizeAll()
        {

            //var exe = LogApplication.Agent.GetCurrentDir();
            var dir = FileEndPointManager.MyMainDirectory;//LogApplication.Agent.GetCurrentDir();//System.IO.Path.GetDirectoryName(exe);


            string drive = System.IO.Path.GetPathRoot(dir);
            string driveLetter = drive.First().ToString();

            var param = string.Format("cd /{0} {1}\\Bat\\exe", driveLetter, dir);

            var newValues = new Dictionary<string, string>
            {
                { "##Home##", param}
               // { "##.exe##", exe + " -isAutoRun true" } //with parameter
            };


            //var newValues = new Dictionary<string, string>
            //{
            //    //{ "username", userName },
            //    { "##.exe##", exe }
            //};
            RunHelper.RunBatFile(EnumFiles.MinimizeAll, newValues);
        }
    }
}
