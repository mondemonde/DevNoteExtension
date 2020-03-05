using Common;
using Common.COMMANDS;
using DevNote.Interface;
using LogApplication.Common.Commands;
using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiCrawler.MODEL
{

    //public class EndWFResult : RunWFCmdParam
    //{
    //    public EndWFResult()
    //    {
    //        CommandName = EnumCmd.EndWFResult.ToString();
    //    }

    //    public  int RetryCount { get; set; }

    //    public  string ResultCode { get; set; }


    //}


    [Obsolete]
    public static class EventResponder
    {

        public static int RetryCount { get; set; }

        public static string ResultCode { get; set; }

        public static bool IsSuccess { get; set; }


        public static CmdParam MyCmd { get; set; }

        [Obsolete]
        public static void Retry()
        {
            ConfigManager config = new ConfigManager();
            var screenshotOnFail = config.GetValue("ScreenshotOnFail");

            if (File.Exists(screenshotOnFail))
            {
                if (RetryCount < 3)
                {
                    RetryCount += 1;
                    File.Delete(screenshotOnFail);

                    BotHttpClient.Log("RETRY FAILED RUN... Retry count is " + RetryCount.ToString(), true);

                    GlobalDef.CurrentDesigner.HandleCmd(MyCmd);
                    return;
                }
            }

           
            //show result to azure event...
            BotHttpClient.Log("Done WF Run... Retry count is " + RetryCount.ToString(), false);
            RetryCount = 0;
            MyCmd = null;

            //xTODO.. create azure event here...




        }


    }


}
