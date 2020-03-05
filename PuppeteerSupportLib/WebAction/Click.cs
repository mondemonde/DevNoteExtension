using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuppetSupportLib.Katalon;

namespace PuppetSupportLib.WebAction
{
    public class PuppetClick : BaseAction
    {
        public PuppetClick(TestCaseSelenese katalonxml):base(katalonxml)
        {
           
        }

        //convert custom to testkatalon
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
            var script = string.Format("await page.click('{0}'", interpreter.FormatSelector(MyAction.target));
            script = script + Environment.NewLine;

            return script;
        }
    }
}
