using PuppetSupportLib;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport
{
    public class WaitForInvisible : BaseAction
    {
        public WaitForInvisible(TestCaseSelenese katalonxml) : base(katalonxml)
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

            var script = string.Format("waitForInvisible(\"{0}\", {1});", target, value);
            script = script + Environment.NewLine;

            return script;
        }
    }
}
