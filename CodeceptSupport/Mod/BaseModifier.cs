using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport.Mod
{
   public abstract class BaseModifier
    {
        //public BaseModifier(IInterpreter it)
        //{
        //    It = it;
        //}
       // public IInterpreter It { get; set; }
       public BaseModifier()
        {
            NonSematics = new List<string> { "#",".",
                "{ css:", "{css:",
                "{ xpath:", "{xpath:","//",
                "{ id:" ,"{id:","{name:","{ name:"};
        }
        public List<string> HeaderString { get; set; }
        public string OriginalString { get; set; }
        public abstract CodeceptAction Modify(CodeceptAction action);

        public string ExtractSelector(CodeceptAction action)
        {
            //bool isFound = false;
            string header = string.Empty;
            foreach (string head in HeaderString)
            {
                if (action.Script.StartsWith(head))
                {
                    header = head;
                    break;
                }
            }

            //var split = args.Split(new string[] { "``" }, StringSplitOptions.None);
            //step# _8.5 action.Script.Split(new string[] { ";I." }, StringSplitOptions.None);
            string[] split = action.Script.Split(new string[] { ";I." }, StringSplitOptions.None);
            var target = split[0];

            split = target.Split(new string[] { ")." }, StringSplitOptions.None);
            target = split[0];

            if (target.Last() == ')')
            {
                target = target.Substring(0, target.Length - 1);
            }
            target = target.Replace(header, string.Empty).Trim();
            return target;
        }

        List<string> NonSematics { get; set; }
        bool semantic;
        public bool IsSemanticLocator(string target)
        {
            target = target.Replace("'", string.Empty);
            target = target.Replace("\"", string.Empty);
            semantic = true;

            foreach(string restriction in NonSematics)
            {
                if(target.StartsWith(restriction))
                {
                    semantic = false;
                    return semantic;
                }
            }

            return semantic;

        }

    }
}
