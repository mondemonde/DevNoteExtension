using PuppetSupportLib;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;
using System;

namespace CodeceptSupport
{
    public class WaitForElement : BaseAction
    {
        public WaitForElement(TestCaseSelenese katalonxml) : base(katalonxml)
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
            string target = MyAction.target;
            string value = MyAction.value;

            var script = string.Format("waitForElement(\"{0}\", {1});", target, value);
            script = script + Environment.NewLine;

            return script;
        }
    }
}
