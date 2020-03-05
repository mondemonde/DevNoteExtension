using Common.Policy;
using DevNote.Interface;
using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class GlobalDesigner
    {
        public static async Task<bool> IsDesignerReady()
        {
           
                bool result = false;

                var response = await BotHttpClient.TaskHttpGetToDesigner("Hi");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    var hello = response.Content.ReadAsStringAsync().Result;
                    BotHttpClient.WriteDevBotResponse("IsDesinerReady: \n" + hello);
                    result = true;//Convert.ToBoolean(hello);
                }
                else
                    result = false;

                return result;
          
        }

        //STEP_.Player #891 RESULTS
        public static async Task CreateWFOutput()
        {
            //var IsWeb= AutoPlayPolicy.AssertWFOutputCreatedByPlayer();
           await FileEndPointManager.CreateOutputWF();
        }


        public static bool IsWFScriptDone { get; set; }

        public static void ResetEvents()
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
            AutoPlayPolicy.AssertAllWFInputRemoved();



        }

    }
}
