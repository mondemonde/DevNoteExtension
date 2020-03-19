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
    class ScrollTo : BaseAction
    {
        public ScrollTo(TestCaseSelenese katalonxml) : base(katalonxml)
        {

        }

        public override TestCaseSelenese Map(object customAction)
        {
            var act = (TestCaseSelenese)customAction;
            //do convettion here..
            //..
            //.
            return act;
        }

        public override string Script(IInterpreter interpreter)
        {
            var target = MyAction.target.Replace("id=", "#");

            var script = string.Format("scrollToElement(\"{0}\")", target);
            script += Environment.NewLine;

            return script;
        }
    }
}
