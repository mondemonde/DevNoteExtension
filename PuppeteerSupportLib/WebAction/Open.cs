using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuppetSupportLib.Katalon;

namespace PuppetSupportLib.WebAction
{

    //open
    public class PuppetGoTo : BaseAction
    {
        public PuppetGoTo(TestCaseSelenese katalonxml) : base(katalonxml)
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
            // await page.goto('https://docs.microsoft.com/en-us/dotnet/api/system.threading.timeout.infinitetimespan?view=netframework-4.8')
            var script = string.Format("await page.goto('{0}')", MyAction.target);
            script += Environment.NewLine + "await navigationPromise" + Environment.NewLine;

            return script;
        }
    }


}
