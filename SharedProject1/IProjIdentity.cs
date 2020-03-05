using Common;
using LogApplication.Common.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{
    public interface IProjIdentity:IToken
    {
        long ProjId { get; set; }

        //IDevNoteBrowser ChromeBrowser { get; set; }

       //IChromeIdentity ChromeIdentity { get; }
        IBot  MyDesigner { get; set; }

       // IArmPlayer CurrentLeftArm { get; set; }

        Task<CmdParam> DoCmd(CmdParam command);

        IArmPlayer CreateCodeCeptArm();

        void CloseWindow();


    }
}
