using BaiTextFilterClassLibrary;
using PuppetSupportLib;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport
{
    public class GrabValueAndEnd : BaseAction
    {
        public GrabValueAndEnd(TestCaseSelenese katalonxml) : base(katalonxml)
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
            //string result = string.Empty;
            var content = MyAction.target.ToString();
            var textContent = MyAction.value;
            string grabAsyncParams = "";
            string target = interpreter.FormatSelector(MyAction.target);

            //#0#Write issued company-owned equipment, devices, and tools here#0#172###578Delay3
            //#0#Write issued company-owned equipment, devices, and tools here#0#172###578
            var delimitter = Keywords.NoDelimiter;
            string[] splitDelay = textContent
                .Split(new string[] { delimitter }, StringSplitOptions.None);

            //#0#Write issued company-owned equipment, devices, and tools here#0#172###578
            var firstPart = splitDelay.First();
            string result = string.Empty;

            if (textContent.Contains(Keywords.GrabMultiDelimiter))
            {
                grabAsyncParams = ", true";
            }

            target += grabAsyncParams;

            if (string.IsNullOrEmpty(textContent) || !textContent.Contains("#1#"))
            {
                if (content.StartsWith("link="))
                {
                    var script = string.Format("grabAsync({0})", target);
                    script += Environment.NewLine;
                    result = script;
                }
                else
                {
                    var script = string.Empty;//string.Format("click('{0}')", target);
                    var prefix = string.Format("{0};MyGrabValue = await I.", @"say('MyGrabValue:')");

                    if (target.StartsWith("{"))//json
                    {
                        //alternately use mouseClickXpathToGrabValue
                        script = string.Format("grabAsync({0})", target);
                    }
                    else if (target.StartsWith("'"))//json
                    {
                        script = string.Format("grabAsync({0})", target);
                    }
                    else if (target.StartsWith("concat("))//json
                    {
                        script = string.Format("grabAsync({0})", target);
                    }
                    else if (target.StartsWith("grabAsync("))//json
                    {
                        script = string.Format("grabAsync({0})", target);
                    }
                    else
                    {
                        script = string.Format("grabAsync('{0}')", target);
                    }
                    script += Environment.NewLine;
                    result = prefix + script;
                }
            }
            else
            {
                //get the text value in #0#
                delimitter = "#1#";
                string[] splitScript = textContent.Split(new string[] { delimitter }, StringSplitOptions.None);

                var xyPart = splitScript.Last();

                delimitter = Keywords.NoDelimiter;
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
