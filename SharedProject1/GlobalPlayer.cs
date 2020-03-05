using Common.Policy;
using DevNote.Interface;
using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common
{
    public static class GlobalPlayer
    {
        public static bool IsFailedResult
        {
            get
            {
                ConfigManager config = new ConfigManager();
                var screenshotOnFail = config.GetValue("ScreenshotOnFail");

                if (File.Exists(screenshotOnFail))
                {


                    //BotHttpClient.Log("RETRY FAILED RUN... Retry count is " + RetryCount.ToString(), true);

                    // GlobalDef.CurrentDesigner.HandleCmd(MyCmd);
                    return true;

                }
                else
                    return false;
            }
        }

        public static void ResetResult()
        {
            ConfigManager config = new ConfigManager();
            var screenshotOnFail = config.GetValue("ScreenshotOnFail");

            if (File.Exists(screenshotOnFail))
            {


                //BotHttpClient.Log("RETRY FAILED RUN... Retry count is " + RetryCount.ToString(), true);
                // GlobalDef.CurrentDesigner.HandleCmd(MyCmd);
                File.Delete(screenshotOnFail);  

            }


            AutoPlayPolicy.AssertWFOutputRemoved();

        }


      


    }
}
