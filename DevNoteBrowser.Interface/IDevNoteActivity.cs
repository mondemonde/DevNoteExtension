using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{
  public  interface IFrontActivity
    {
        IFrontWF ParentWF { get; set; }
    }
}
