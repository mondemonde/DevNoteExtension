using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{
   public class VariableExtension
    {
        public VariableExtension()
        {
            NewVaues = new Dictionary<string, string>();
        }
       // Dictionary<string, string>
        public Dictionary<string,string> NewVaues { get; set; }
        public bool IsAPI { get; set; }

        //public CmdParam CmdOutPut { get; set; }

    }
}
