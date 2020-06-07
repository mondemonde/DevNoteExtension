using System;
using System.Linq;
using PuppetSupportLib;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;

namespace CodeceptSupport
{
    public class Click : BaseAction
    {
        public Click(TestCaseSelenese katalonxml) : base(katalonxml)
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

            var textContent = MyAction.value;
            if (string.IsNullOrEmpty(textContent) || !textContent.Contains("#1#"))
            {
                if (content.StartsWith("link="))
                {
                    var script = string.Format("clickLink({0})"
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
                        script = string.Format("click({0})", newTarget);
                    }
                    else if (newTarget.StartsWith("'"))//json
                    {
                        script = string.Format("click({0})", newTarget);
                    }
                    else if (newTarget.StartsWith("concat("))//json
                    {
                        script = string.Format("click({0})", newTarget);
                    }
                    else if (newTarget.StartsWith("click("))//json
                    {
                        script = string.Format("click({0})", newTarget);
                    }
                    else
                    {
                        script = string.Format("click('{0}')", newTarget);
                    }
                    script = script + Environment.NewLine;
                    result = script;
                }
            }//no textContent
            else if(textContent.Contains("#1#")) //default use text in button like click('Login');
            {
                string delimitter = "#1#";
                string[] splitScript = textContent.Split(new string[] { delimitter }, StringSplitOptions.None);

                if(textContent.StartsWith("#0#"))
                {
                    //"mouseClick('Price high to low#0#1030###346Delay10')"    

                    delimitter = "Delay";
                    string[] splitDelay = splitScript.Last()
                        .Split(new string[] { delimitter }, StringSplitOptions.None);

                    var script1 = string.Format("mouseClick('{0}');I.wait({1})", splitDelay.First(),splitDelay.Last());
                    result = script1;
                }
                else
                {
                    delimitter = "#";
                    string[] splitScript2 = splitScript.Last().Split(new string[] { delimitter }, StringSplitOptions.None);
                    
                    var  script2 = string.Format("click('{0}')", splitScript2.First());

                    result = script2;
                }               
            }
            return result;
        }
    }
}
