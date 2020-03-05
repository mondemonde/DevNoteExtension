using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevNote.Interface;
using LogApplication.Common.Commands;
using WFHostingApplication.COMMANDS;

namespace Common.COMMANDS
{
    //ActivityStep.1 CodeceptCmdParam
    public class UpdateMainViewCmdParam : CmdParam
    {
        #region Acitiviy Properties
        //public string XamlFullPath { get; set; }
        public string EventName { get; set; }

        public string username { get; set; }
        public string password { get; set; }


        #endregion

        //public RunWFCmdParam(string alias)
        //{
        //    CommandName = EnumCmd.RunWF.ToString();
        //    XamlFullPath = alias;
        //    Alias = alias;
        //    //self reference error 
        //    //Payload = this;
        //    SavePayload();
        //}

        public UpdateMainViewCmdParam()
        {
            CommandName = EnumCmd.UpdateMainView.ToString();
            //XamlFullPath = alias;           
            //self reference error 
            //Payload = this;
            // SavePayload();
            FontSize = 8;
            Duration = 5;
        }

        public  int FontSize { get; set; }

        public  int Duration { get; set; }


        public  string Message { get; set; }
        public bool IsCentered { get; set; }

        public string StateAKA { get; set; }

       // string message, string status, int fontsize = 25,int duration = 5,bool isCentered = false

    }

}
