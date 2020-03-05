using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevNote.Interface
{
    public class JSPayload
    {
        public int posX { get; set; }
        public int posY { get; set; }
        public string TextToFind { get; set; }
        public string InnerHTML { get; set; }
        public string OuterHML { get; set; }
    }
}
