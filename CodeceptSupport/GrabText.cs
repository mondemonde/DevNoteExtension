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
    public class GrabText : BaseAction
    {

        public GrabText(TestCaseSelenese katalonxml) : base(katalonxml)
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
            string result = string.Empty;
            string locator = string.Empty;

            var content = MyAction.target.ToString();
            //if (content.StartsWith("link="))
            //{
            //    locator = interpreter.FormatSelector(MyAction.target);
            //    var script = string.Format("waitForElement({0})", locator);
            //    //script = script + Environment.NewLine;

            //    result = script;
            //}
            //else
            //{
            //    string newTarget = interpreter.FormatSelector(MyAction.target);

            //    locator = string.Format("'{0}'",newTarget);
            //    //see('submit')
            //    var script = string.Format("waitForElement({0})", locator);

            //    if (newTarget.StartsWith("{"))//json
            //    {
            //        locator = newTarget;
            //        script = string.Format("waitForElement({0})", newTarget);
            //    }
            //    //script = script + Environment.NewLine;
            //    result = script;
            //}


            if (content.StartsWith("link="))
            {
                var script = string.Format("see({0})"
                , interpreter.FormatSelector(MyAction.target));
                script = script + Environment.NewLine;

                result = script;
            }
            else
            {
                var newTarget = interpreter.FormatSelector(MyAction.target);

                var script = string.Empty;//string.Format("click('{0}')", newTarget);

                if (newTarget.StartsWith("{"))//json
                {
                    script = string.Format("waitForElement({0})", newTarget);
                }
                else if (newTarget.StartsWith("'"))//json
                {
                    script = string.Format("waitForElement({0})", newTarget);
                }
                else if (newTarget.StartsWith("concat("))//json
                {
                    script = string.Format("waitForElement({0})", newTarget);
                }
                //else if (newTarget.StartsWith("waitForElement("))//json
                //{
                //    script = string.Format("see({0})", newTarget);
                //}


                else
                {
                    script = string.Format("see('{0}')", newTarget);

                }
                script = script + Environment.NewLine;
                result = script;
            }


            string finalResult = string.Format("{0};MyGrabValue = await I.grabAsync({1});", result.Trim(), interpreter.FormatSelector(MyAction.target));//locator.Trim());

            finalResult = finalResult + Environment.NewLine;
            return finalResult;
        }
    }
}
