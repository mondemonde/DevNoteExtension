using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuppetSupportLib;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;

namespace CodeceptSupport
{
    class CaptureScreenshot : BaseAction
    {
        public CaptureScreenshot(TestCaseSelenese katalonxml) : base(katalonxml)
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
            string fileName = MyAction.target;
            string extension = Path.GetExtension(fileName);

            if (extension.ToLower() != ".png")
            {
                fileName += ".png";
            }

            var script = string.Format("saveScreenshot('{0}')", fileName);
            script += Environment.NewLine;

            return script;
        }
    }
}
