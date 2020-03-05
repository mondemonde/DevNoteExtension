using LogApplication.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface.Common
{
  public  class BotStepCmd:CmdParam
    {
     public BotStepCmd(EnumMessageFrom source = EnumMessageFrom.CodeCeptArm,EnumMessageTo target = EnumMessageTo.Chrome):base()
        {
            CommandName = EnumCmd.BotStep.ToString();
            MessageFrom = source.ToString();
            MessageTo = target.ToString();
        }
        public string Script { get; set; }

    }
}
