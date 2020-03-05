using Common;
using DevNote.Interface.Common;
using LogApplication.Common.Commands;
using MyCommonLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{

    public interface IArmPlayer : IToken
    {
      int ArmId { get; set; }

      bool IsSessionLifeSpan { get; set; }

      bool IsArmReady { get; set; }

      EnumPlayStatus Status { get; set; }


        //Task<ServiceResponse<List<CmdParam>>> RunWorkflowAsync(string file, VariableExtension Ext);
        // Task<ServiceResponse<List<CmdParam>>> RunCodeCeptAsync(string file, VariableExtension Ext);


    }


}
