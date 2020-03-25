using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player.Models
{
    public class EventTag
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public string Department { get; set; }
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourcePath { get; set; }
        public bool InActive { get; set; }
        public int VersionNo { get; set; }
        public string FileName { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
