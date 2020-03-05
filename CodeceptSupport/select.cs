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
    public class SelectOption : BaseAction
    {

        public SelectOption(TestCaseSelenese katalonxml) : base(katalonxml)
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
            var selValue = MyAction.value.Trim();
            //if (content.StartsWith("link="))
            //{
            //    var script = string.Format("clickLink({0})"
            //    , interpreter.FormatSelector(MyAction.target));
            //    script = script + Environment.NewLine;

            //    result = script;
            //}
            //else
            //{
               
            //}
            var newTarget = interpreter.FormatSelector(MyAction.target);

            var script = string.Empty;//string.Format("click('{0}')", newTarget);

            //label=Manager
            var split = selValue.Split('=');
            selValue = split.Last(); //manager

            if (!selValue.StartsWith("'"))//json
            {
                selValue = string.Format("'{0}'", selValue);//'manager'
            }


            if (newTarget.StartsWith("{"))//json
            {
                script = string.Format("selectOption({0},{1})", newTarget, selValue);
            }
            else if (newTarget.StartsWith("'"))//json
            {
                script = string.Format("selectOption({0},{1})", newTarget, selValue);
            }
            else
            {
                script = string.Format("selectOption('{0}',{1})", newTarget, selValue);

            }
            script = script + Environment.NewLine;
            result = script;
            return result;
        }
    }
}
