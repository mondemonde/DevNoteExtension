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
    public class GoTo : BaseAction
    {

        public GoTo(TestCaseSelenese katalonxml) : base(katalonxml)
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
            //await page.click('.container > #mvcforum-nav > .nav > li > .auto-logon')
            var script = string.Format("amOnPage('{0}')"
                , MyAction.target);
            script = script + Environment.NewLine;

            return script;
        }
    }
}
