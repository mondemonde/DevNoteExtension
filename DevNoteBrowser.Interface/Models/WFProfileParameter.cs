using MyCommonLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface.Models
{
  public  class WFProfileParameter:BaseModel
    {
       
        public int WFProfileId { get; set; }
        public string PropertyName { get; set; }
        public string MappedTo_Input_X { get; set; }

        public string DefaultValue { get; set; }


    }
}
