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
    public class MouseOver : BaseAction
    {

        public MouseOver(TestCaseSelenese katalonxml) : base(katalonxml)
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

            var content = MyAction.target.ToString();
            if (content.StartsWith("link="))
            {
                var script = string.Format("moveCursorTo({0})"
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
                    script = string.Format("moveCursorTo({0})", newTarget);
                }
               else if (newTarget.StartsWith("'"))//json
                {
                    script = string.Format("moveCursorTo({0})", newTarget);
                }
               else if (newTarget.StartsWith("concat("))//json
                {
                    script = string.Format("moveCursorTo({0})", newTarget);
                }
                else if (newTarget.StartsWith("mouseOver("))//json
                {
                    script = string.Format("moveCursorTo({0})", newTarget);
                }
                else if (newTarget.StartsWith("mouseOverAndWait("))//json
                {
                    script = string.Format("moveCursorTo({0})", newTarget);
                }

                else
                {
                     script = string.Format("moveCursorTo('{0}')", newTarget);

                }
                script = script + Environment.NewLine;
                result = script;
            }
            return result;
        }
    }
}
