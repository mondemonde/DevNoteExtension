using Common;
using DevNote.Interface.Common;
using PuppetSupportLib.Katalon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport
{
   
  public  class CodeceptAction :TestCaseSelenese
    {
        public string Script { get; set; }
        public int OrderNo { get; set; }
        public bool IsFailed  { get; set; }

        public EnumPlayStatus  PlayStatus { get; set; }

    }
}
