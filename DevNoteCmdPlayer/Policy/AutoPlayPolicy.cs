using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNoteCmdPlayer.Policy
{
   public static class AutoPlayPolicy
    {
     public static bool  AssertRetry(int retry,int limit)
        {
          
            if (retry > limit)
                return true ;
            else
                return false;

        }

      public static bool AssertMyGrabValueRemoved()
        {

            ConfigManager config = new ConfigManager();
            var endPointFolder = config.GetValue("Project2EndPointFolder");
            var file = Path.Combine(endPointFolder, Common.FileEndPointManager.MyGrabValueFile);

            if (File.Exists(file))
                File.Delete(file);
            


            return true;


        }

        public static bool AssertResultTxtRemoved()
        {

            ConfigManager config = new ConfigManager();
            var endPointFolder = config.GetValue("Project2EndPointFolder");
            var file = Path.Combine(endPointFolder, Common.FileEndPointManager.MyGrabValueFile);

            if (File.Exists(file))
                File.Delete(file);



            return true;


        }

        public static bool AssertResultTxtExist()
        {

            ConfigManager config = new ConfigManager();
            var endPointFolder = config.GetValue("Project2EndPointFolder");
            var resultTxt = Path.Combine(endPointFolder, "result.txt");

            if (File.Exists(resultTxt))
                return true;
            else
                return false;


        }


    }
}
