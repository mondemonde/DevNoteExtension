using DevNote.Interface;
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
   public class Codecept:Puppet
    {
        //step# 83 interpreter.Script
        //STEP.Player #800 SCRIPT
        public new static string Script(TestCaseSelenese cmd, IInterpreter it)
        {
            string result = string.Empty;
            //_STEP_.Player  switch (cmd.command)
            switch (cmd.command)
            {
                case "#":
                    result = new Comment(cmd).Script(it);
                    break;
                case "open":
                    result = new GoTo(cmd).Script(it);
                    break;
                case "click":
                    result = new ClickAndType(cmd).Script(it);
                    break;
                case "doubleClick":
                    result = new ClickAndType(cmd).Script(it);
                    break;
                case "pause":
                    result = new WaitDelay(cmd).Script(it);
                    break;
                case "type":
                    result = new TypeIn(cmd).Script(it);
                    break;
                case "select":
                    result = new SelectOption(cmd).Script(it);
                    break;
                case "sendKeys":
                    result = new SendKey(cmd).Script(it);
                    break;
                case "store":
                    result = new StoreToVariable(cmd).Script(it);
                    break;
                case "submit":
                    result = "";
                    break;
                case "addSelection":
                    result = new SelectOption(cmd).Script(it);
                    break;
                case "captureEntirePageScreenshot":
                    result = new CaptureScreenshot(cmd).Script(it);
                    break;
                case "scrollTo":
                    result = new ScrollTo(cmd).Script(it);
                    break;
                default:
                    result = new NotSupportedAction(cmd).Script(it);
                    break;
            }
            return result;
        }
    }
}
