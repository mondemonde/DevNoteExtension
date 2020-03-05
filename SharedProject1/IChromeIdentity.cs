using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{
    public interface IChromeIdentity
    {
        int RemoteDebuggerPort { get; set; }
        string RemoteDebuggerAddress { get; set; }
        bool IsHeadless { get; set; }

        string WebAPI { get; set; }


    }
}
