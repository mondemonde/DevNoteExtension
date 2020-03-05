using PuppetSupportLib.Katalon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuppetSupportLib.WebAction
{
   public class Puppet
    {
        public static  string  Script(TestCaseSelenese cmd,IInterpreter it)
        {
            string result = string.Empty;

            switch (cmd.command)
            {
                case "open":
                    result = new PuppetGoTo(cmd).Script(it);
                    break;

                case "click":
                    result = new PuppetClick(cmd).Script(it);                   
                    break;



                default:
                    break;
            }

            return result;
        }
    }
}
