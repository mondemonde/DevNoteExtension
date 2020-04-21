using System;
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
            string value = MyAction.value;

            var script = string.Format("say('Delay');I.wait({0});", value);            
            script = script + Environment.NewLine;

            return script;
        }
    }
}
