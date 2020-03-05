using Common;
using Common.COMMANDS;
using LogApplication.Common.Commands;
using MyCommonLib.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{

    public interface IBot : IToken
    {

        Task<ServiceResponse<List<CmdParam>>> RunWorkflowAsync(string file, VariableExtension Ext);

        //private
        Task<HttpResponseMessage> HandleCmd(CmdParam param);

        IFrontWF FrontWF { get; }

        RunWFCmdParam CurrentCmd {get;set;}


        string DevNoteAPI { get; set; }
        string ArmAPI { get;set; }
        string ChromeAPI { get; set; }

        void RunNewWF();

        void CloseWindow();

        bool IsRunningWF { get; }

        Process Process { get; set; }



    }

   
}
