using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumSupportLib.Models
{

    public class Command
    {
        public string id { get; set; }
        public string comment { get; set; }
        public string command { get; set; }
        public string target { get; set; }
        public List<object> targets { get; set; }
        public string value { get; set; }
    }

    public class Test
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Command> commands { get; set; }
    }

    public class Suite
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool persistSession { get; set; }
        public bool parallel { get; set; }
        public int timeout { get; set; }
        public List<string> tests { get; set; }
    }

    public class RootObject
    {
        public string id { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public List<Test> tests { get; set; }
        public List<Suite> suites { get; set; }
        public List<string> urls { get; set; }
        public List<object> plugins { get; set; }
    }

}
