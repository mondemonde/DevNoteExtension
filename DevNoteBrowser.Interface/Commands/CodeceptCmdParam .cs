using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevNote.Interface;
using WFHostingApplication.COMMANDS;

namespace Common.COMMANDS
{
    //ActivityStep.1 CodeceptCmdParam
    public class RunWFCmdParam : CodeceptCmdParam
    {
        #region Acitiviy Properties
        //public string XamlFullPath { get; set; }
        public string EventName { get; set; }

        //public string username { get; set; }
        //public string password { get; set; }
        public Dictionary<string, string> EventParameters { get; set; }

        public string OuputResponse { get; set; }

        public string GuidId { get; set; }

        public int RetryCount { get; set; }

        public string ErrorCode { get; set; }


        #endregion


        public string EventFilePath { get; set; }


        public RunWFCmdParam()
        {
            CommandName = EnumCmd.RunWF.ToString();
            //XamlFullPath = alias;           
            //self reference error 
            //Payload = this;
            SavePayload();
        }


        public new void SavePayload()
        {
            RequestDate = DateTime.Now;
            Payload = this.Payload;
        }
    }

}
