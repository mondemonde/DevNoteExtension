using LogApplication.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{
  public interface IBotHost
    {
        void NewBrowser();

        Task<HttpResponseMessage> HandleCmd(CmdParam param);

        IBot MyDesigner { get; set; }

        Task Log(string msg);

        string ExternalAPI { get; set; }

       //IProjIdentity CurrentProject { get; set; }


    }
}
