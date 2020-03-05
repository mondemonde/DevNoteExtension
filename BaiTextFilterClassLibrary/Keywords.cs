using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTextFilterClassLibrary
{
   public static class Keywords
    {
        public const string declareVariable = "say('DECLARE');var ";
        public const string useVariable = "say('USE_VAR');";

        //TIP: Keywords
        public const string clickAndTypeDelimiter = "Delay3_";
        public const string TypeAndTabDelimiter = "Delay3_?";
        public const string JustTypeOnlyDelimiter = "Delay3_@";

        public const string ClickAndEndDelimiter = "Delay3_END";



    }
}
