using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{
  public  interface IFrontWF
    {
        string CurrentFilePath { get; set; }
        void Load(string fileToLoad);
        void Save();
        bool IsFrontWFShuttingDown { get; set; }
    }
}
