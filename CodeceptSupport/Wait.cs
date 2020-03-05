using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuppetSupportLib;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;

namespace CodeceptSupport
{
    public class WaitDelay : BaseAction
    {

        public WaitDelay(TestCaseSelenese katalonxml) : base(katalonxml)
        {

        }

        public override TestCaseSelenese Map(object customAction)
        {
            //throw new NotImplementedException();
            var act = (TestCaseSelenese)customAction;
            //do convettion here..
            //..
            //.


            return act;
        }

        
        public override string Script(IInterpreter interpreter)
        {
           var targetValue = MyAction.target;
            var target = Convert.ToInt32(targetValue);

            target = target / 1000;



            var script = string.Format("say('Delay');I.wait({0});",target.ToString());            

            script = script + Environment.NewLine;

            return script;
        }
    }
}
