using LogApplication.Common.Config;
using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport.Mod
{
    public class SendKeyModifier : BaseModifier
    {

        //public ClickModifier(IInterpreter it):base(it)
        //{
        //    HeaderString = new List<string> { "click(" , "clickLink(" };
        //}

        public int DefaultWait { get; set; }

        public SendKeyModifier() : base()
        {
            //HeaderString = new List<string> { "click(", "clickLink(", "fillField(" };
            HeaderString = new List<string> { "pressKey('" };
            ConfigManager config = new ConfigManager();

            DefaultWait = Convert.ToInt32(config.GetValue("Click_Default_Wait"));


        }

        public override CodeceptAction Modify(CodeceptAction action)
        {
            //throw new NotImplementedException();
            bool isFound = false;
            foreach(string head in HeaderString)
            {
               if(action.Script.StartsWith(head))
                {
                    isFound = true;
                    break;
                }
            }

           if(isFound)
            {
                //var target =ExtractSelector(action);

                //pressKey('Enter');
                var split = action.Script.Split('\'');

                //wait for use square barcket
                var keys = split[1];

                //fillField({id:'usernamebox'}
                OriginalString = action.Script;
                var newScript = OriginalString;

                var suffix = string.Empty;
                if (keys.ToUpper().StartsWith("ENTER"))
                {


                    suffix = string.Format("I.wait({0})", DefaultWait.ToString());
                    newScript = string.Format("{0};{1}",  OriginalString,suffix);

                }           
              
                Console.WriteLine(newScript);
                action.Script = newScript;

            }

            return action;
        }

        //string Script(CodeceptAction MyAction)
        //{
        //    //await page.click('.container > #mvcforum-nav > .nav > li > .auto-logon')
        //    string result = string.Empty;

        //    var content = MyAction.target.ToString();
        //    if (content.StartsWith("link="))
        //    {
        //        var script = string.Format("clickLink({0})"
        //        , It.FormatSelector(MyAction.target));
        //        script = script + Environment.NewLine;

        //        result = script;
        //    }
        //    else
        //    {
        //        var newTarget = It.FormatSelector(MyAction.target);
        //        var script = string.Format("click('{0}')", newTarget);

        //        if (newTarget.StartsWith("{"))//json
        //        {
        //            script = string.Format("click({0})", newTarget);
        //        }
        //        script = script + Environment.NewLine;
        //        result = script;
        //    }
        //    return result;
        //}



    }
}
