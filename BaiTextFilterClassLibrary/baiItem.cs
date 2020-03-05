using MyCommonLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTextFilterClassLibrary
{
    public class WebItem: BaseModel
    {
        public string ReferenceUrl { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }

    }

    public class FindItem
    {
        public FindItem()
        {
            ListFound = new List<string>();
            IsFound = false;
        }
        public List<string> ListFound { get; set; }
        public bool IsFound { get; set; }
    }
}
