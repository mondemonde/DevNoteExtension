using DevNote.Interface;
using System;
using System.Collections.Generic;
using System.Text;
 

namespace Common
{
    public class ChromeIdentity : IChromeIdentity
    {
        public int RemoteDebuggerPort { get ; set ; }
        public string RemoteDebuggerAddress { get ; set ; }
        public bool IsHeadless { get; set ; }
    }
}
