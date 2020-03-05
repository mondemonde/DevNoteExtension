using WFHostingApplication;
using LogApplication.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevNote.Interface;

namespace WFHostingApplication.COMMANDS
{
    //ActivityStep.1 CodeceptCmdParam
    public class CodeceptCmdParam : CmdParam
    {
        #region Acitiviy Properties
        public string JSFullPath { get; set; }

        
        #endregion

        public CodeceptCmdParam(string path)
        {
            CommandName = EnumCmd.Codecept.ToString();            
            JSFullPath = path;
            //Payload = this;
            //SavePayload();
        }

        public CodeceptCmdParam()
        {
            CommandName = EnumCmd.Codecept.ToString();
            //JSFullPath = path;
            //Payload = this;
           // SavePayload();
        }

        public void SavePayload()
        {
            RequestDate = DateTime.Now;
            Payload = this.Payload;
        }
    }

}
