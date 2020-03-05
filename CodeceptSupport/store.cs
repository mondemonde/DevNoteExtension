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
    public class StoreToVariable : BaseAction
    {

        public StoreToVariable(TestCaseSelenese katalonxml) : base(katalonxml)
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




            //value of the variable..

            var content = MyAction.target;
            var variableName = MyAction.value;
            var script = string.Empty;

            if (content.StartsWith("${"))
            {
                //default was get the value frm  I.MyGrabValue
                //targetValue = "I.MyGrabValue";
                // script = string.Format("MyGrabValue = await I.getMyGrabValue();I.fillField({0}, {1});", interpreter.FormatSelector(MyAction.target), targetValue);

                variableName = content.Replace("${", string.Empty);
                variableName = variableName.Replace("}", string.Empty);
                variableName = variableName.Trim();

                //say('ASSIGN'); {1} = {2};I.say('END_ASSIGN')
                //script = string.Format("{0};I.fillField({1}, {2});", Keywords.useVariable, interpreter.FormatSelector(MyAction.target), variableName);
                script = string.Format("{0}{1} = {2};I.say('END_DECLARE')", Keywords.declareVariable, variableName, content);



            }
            else

            {
                 script = string.Format("{0}{1} = '{2}';I.say('END_DECLARE')", Keywords.declareVariable, variableName, content);
                //script = script + Environment.NewLine;                     
            }
 
            var  finalResult = script + Environment.NewLine;
            return finalResult;
        }
    }
}
