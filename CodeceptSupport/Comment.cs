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
    public class Comment : BaseAction
    {

        public Comment(TestCaseSelenese katalonxml) : base(katalonxml)
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
            //var variableName = MyAction.value;

            var script = string.Format("say('{0}')", content);


            var finalResult = script + Environment.NewLine;
            return finalResult;
        }
    }
}
