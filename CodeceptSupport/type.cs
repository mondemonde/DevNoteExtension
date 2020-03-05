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
    public class TypeIn : BaseAction
    {

        public TypeIn(TestCaseSelenese katalonxml) : base(katalonxml)
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
            var script = string.Empty;
           //await page.click('.container > #mvcforum-nav > .nav > li > .auto-logon')
           var targetValue = MyAction.value;
            var content = MyAction.target;

            if (targetValue.StartsWith("${"))
            {
                //default was get the value frm  I.MyGrabValue
                //targetValue = "I.MyGrabValue";
                // script = string.Format("MyGrabValue = await I.getMyGrabValue();I.fillField({0}, {1});", interpreter.FormatSelector(MyAction.target), targetValue);

                var variableName = targetValue.Replace("${", string.Empty);
                variableName = variableName.Replace("}", string.Empty);
                variableName = variableName.Trim();

                //say('ASSIGN'); {1} = {2};I.say('END_ASSIGN')
                script = string.Format("{0};I.fillField({1}, {2});",Keywords.useVariable,  interpreter.FormatSelector(MyAction.target), variableName);



            }
            else
            {
                targetValue = MyAction.value;

                //TIP: variable declartion is handle by Modifier
                //script = string.Format("{0}{1} = {2};I.say('END_ASSIGN');I.fillField({2}, {1});", Keywords.declareVariable, variableName, interpreter.FormatSelector(MyAction.target));

                script = string.Format("fillField({0}, '{1}')", interpreter.FormatSelector(MyAction.target), targetValue);

            }

            script = script + Environment.NewLine;

            return script;
        }
    }
}
