using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface.Common
{
    public enum EnumPlayStatus
    {

        TimeOut = -10,//or TimedOut

        Faulted = -1, 

        Success = 0,

        Idle=5,

        Playing = 10,

        Retrying = 20
    }
}
