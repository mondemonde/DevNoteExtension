using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaiTextFilterClassLibrary;
using PuppetSupportLib;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;

namespace CodeceptSupport
{
    public class ClickAndType : BaseAction
    {

        public ClickAndType(TestCaseSelenese katalonxml) : base(katalonxml)
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

          if (textContent.Contains(Keywords.ClickAndEndDelimiter))//("Delay3_END"))// do click and type
            {

               return ScriptClickToEnd(interpreter);              

            }


           else if (string.IsNullOrEmpty(textContent) || !textContent.Contains("#1#"))
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

                var xyPart = splitScript.Last();

                if (textContent.StartsWith("#0#"))
                {
                    //"mouseClick('Price high to low#0#1030###346Delay10')"  
                    if (xyPart.Contains(Keywords.JustTypeOnlyDelimiter))//("Delay3_"))// do click and type
                    {
                        delimitter = Keywords.JustTypeOnlyDelimiter;
                        string[] splitDelay = xyPart
                            .Split(new string[] { delimitter }, StringSplitOptions.None);


                        var script1 = string.Format("sendCharacter('{0}');", splitDelay.Last());
                        result = script1;
                     

                    }
                    else if (xyPart.Contains(Keywords.TypeAndTabDelimiter))//("Delay3_"))// do click and type
                    {
                        delimitter = Keywords.TypeAndTabDelimiter;
                        string[] splitDelay = xyPart
                            .Split(new string[] { delimitter }, StringSplitOptions.None);
                        var scriptTab = string.Format("I.pressKey('{0}');", "Tab");

                        var script1 =  string.Format("sendCharacter('{0}');", splitDelay.Last()) + scriptTab;
                        result = script1;

                    }

                    else if (xyPart.Contains(Keywords.clickAndTypeDelimiter))//("Delay3_"))// do click and type
                    {
                        delimitter = Keywords.clickAndTypeDelimiter;
                        string[] splitDelay = xyPart
                            .Split(new string[] { delimitter }, StringSplitOptions.None);

                       
                        var script1 = string.Format("mouseClick('{0}');I.wait(1);I.sendCharacter('{1}');", splitDelay.First(), splitDelay.Last());
                        result = script1;

                        //todo ADD modifier to handle variable input assignment var like...
                        //I.say('step#2');I.say('DECLARE');var input_1_ = 'rgalvez@blastasia.com';I.waitForElement('[id="usernamebox"]',45);I.retry({ retries: 3, maxTimeout: 3000 }).fillField({id:'usernamebox'}, input_1_);I.wait(1);


                    }


                    else
                    {
                        delimitter = "Delay";
                        string[] splitDelay = xyPart
                            .Split(new string[] { delimitter }, StringSplitOptions.None);


                        var script1 = string.Format("mouseClick('{0}');I.wait({1})", splitDelay.First(), splitDelay.Last());
                        result = script1;
                    }
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

        private string ScriptClickToEnd(IInterpreter interpreter)
        {
            //string result = string.Empty;
            var content = MyAction.target.ToString();
            var textContent = MyAction.value;

            //#0#Write issued company-owned equipment, devices, and tools here#0#172###578Delay3_END
            //#0#Write issued company-owned equipment, devices, and tools here#0#172###578

            var delimitter = Keywords.ClickAndEndDelimiter;
            string[] splitDelay = textContent
                .Split(new string[] { delimitter }, StringSplitOptions.None);

            //#0#Write issued company-owned equipment, devices, and tools here#0#172###578
            var firstPart = splitDelay.First();
            string result = string.Empty;

            if (string.IsNullOrEmpty(textContent) || !textContent.Contains("#1#"))
            {

                if (content.StartsWith("link="))
                {
                    var script = string.Format("grabAsync({0})"
                    , interpreter.FormatSelector(MyAction.target));
                    script = script + Environment.NewLine;

                    result = script;
                }
                else
                {
                    var newTarget = interpreter.FormatSelector(MyAction.target);

                    var script = string.Empty;//string.Format("click('{0}')", newTarget);

                    var prefix = string.Format("{0};MyGrabValue = await I.", @"say('MyGrabValue:')");

                    if (newTarget.StartsWith("{"))//json
                    {
                        //alternately use mouseClickXpathToGrabValue
                        script = string.Format("grabAsync({0})", newTarget);
                    }
                    else if (newTarget.StartsWith("'"))//json
                    {
                        script = string.Format("grabAsync({0})", newTarget);
                    }
                    else if (newTarget.StartsWith("concat("))//json
                    {
                        script = string.Format("grabAsync({0})", newTarget);
                    }
                    else if (newTarget.StartsWith("grabAsync("))//json
                    {
                        script = string.Format("grabAsync({0})", newTarget);
                    }


                    else
                    {
                        script = string.Format("grabAsync('{0}')", newTarget);

                    }
                    script = script + Environment.NewLine;
                    result =prefix + script;
                }

            }
            else
            {
                //get the text value in #0#
                delimitter = "#1#";
                string[] splitScript = textContent.Split(new string[] { delimitter }, StringSplitOptions.None);

                var xyPart = splitScript.Last();

                delimitter = Keywords.ClickAndEndDelimiter;
                splitDelay = xyPart
                    .Split(new string[] { delimitter }, StringSplitOptions.None);



                var resultScript = splitDelay.First();// + "Delay1";

                var script1 = string.Format("mouseClickXYToGrabValue('{0}');", resultScript);
                result = script1;

            }


            return result;

         
        }

    }
}
