using DevNote.Interface;
using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Policy
{
    public static class AutoPlayPolicy
    {
        public static bool AssertRetry(int retry, int limit)
        {
            //STEP_.Player AutoPlayPolicy Policy
            if (retry > limit)
                return true;
            else
                return false;

        }

        public static bool AssertMyGrabValueRemoved()
        {

            var endPointFolder = FileEndPointManager.Project2EndPointFolder;


            var file = Path.Combine(endPointFolder, EnumFiles.MyGrabValue);

            if (File.Exists(file))
                File.Delete(file);



            return true;


        }

        public static bool AssertWFOutputRemoved()
        {

            //STEP.Player #865 get local endpoint of Player
            var endPointFolder = FileEndPointManager.Project2EndPointFolder;

            var fileResult = Path.Combine(endPointFolder, EnumFiles.MyResult);
            if (File.Exists(fileResult))
                File.Delete(fileResult);


            //STEP.Player #866 get the main wfoutput endpoint file
            //this was created after result.txt of codecept was created 

            var file = System.IO.Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFOutput);
            if (File.Exists(file))
                File.Delete(file);



            return true;


        }

        public static bool AssertAllWFInputRemoved()
        {
            var eventFiles = Directory.GetFiles(FileEndPointManager.MyEventDirectory
                   , "*", SearchOption.TopDirectoryOnly)
                   .ToList().OrderBy(x => x);
            foreach (string file in eventFiles)
            {
                if (File.Exists(file))
                {
                    try
                    {

                        File.Delete(file);
                    }
                    catch (Exception err)
                    {

                        LogApplication.Agent.LogError(err.Message);
                    }
                }
            }

            return true;
        }


        public static bool AssertWFOutputCreatedByPlayer()
        {

            try
            {
                //STEP.Player #868 write local endpoint of Player
                var endPointPlayerFolder = FileEndPointManager.Project2EndPointFolder;

                var fileResult = Path.Combine(endPointPlayerFolder, EnumFiles.MyResult);

                //if there is result from player
                if (File.Exists(fileResult))
                {
                    return true;

                }
                else
                    return false;
            }
            catch (Exception err)
            {

                BotHttpClient.Log(err.Message, true);
                return false;
            }




        }




        public static bool AssertPlayerResultExist(DateTime started)
        {
            //STEP.Player #865 get local endpoint of Player
            ConfigManager config = new ConfigManager();
            var endPointPlayerFolder = FileEndPointManager.Project2EndPointFolder;

             

            var fileResult = Path.Combine(endPointPlayerFolder, EnumFiles.MyResult);

            if (AssertPlayerTimedOut(started).Result)
            {
               
                var timeOut = Convert.ToInt32(config.GetValue("MaxTimeOutMinutes"));
                File.WriteAllText(fileResult, "TimeOut: " + timeOut + "mins run.");
                return true;
            }


            //if there is result from player
            if (File.Exists(fileResult))
            {
                //File.WriteAllText(file, content);
                // FileEndPointManager.CreateOutputWF(content);
                return true;
            }
            else
                return false;




        }

        public async static Task<bool> AssertPlayerTimedOut(DateTime startRun)
        {
            // Timeout 
            ConfigManager config = new ConfigManager();
            var timeOut = Convert.ToInt32(config.GetValue("MaxTimeOutMinutes"));

            //create result file



            //STEP.Player #803 timeout
            var span = DateTime.Now - startRun;
            //TIP  if ((int)span.TotalMinutes % 30 == 0)    
            if ((int)span.Seconds % 30 == 0)             
            {
                var msg = $"Task is Running :{span.TotalMinutes.ToString()} minutes already.";
                Console.WriteLine(msg);
              await  BotHttpClient.Log(msg);
            }

            if (span.TotalMinutes >= timeOut)
            {
                return true;
            }
            else
                return false;

        }



        //this is created after result.txt of codecept is created 
        public static bool AssertWFOutputExist()
        {
            //STEP.Player #866 get the main wfoutput endpoint file
            var file = System.IO.Path.Combine(FileEndPointManager.MyWaitOneDirectory, EnumFiles.WFOutput);


            //if there is result from player
            if (File.Exists(file))
            {

                //File.WriteAllText(file, content);
                // FileEndPointManager.CreateOutputWF(content);
                return true;
            }
            else
                return false;




        }


    }
}
