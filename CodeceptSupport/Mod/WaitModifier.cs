using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport.Mod
{
    public class WaitModifier : BaseModifier
    {

        //public ClickModifier(IInterpreter it):base(it)
        //{
        //    HeaderString = new List<string> { "click(" , "clickLink(" };
        //}
        public WaitModifier() : base()
        {
            HeaderString = new List<string> { "waitForElement(" };
           // HeaderString = new List<string>();

        }

        public override CodeceptAction Modify(CodeceptAction action)
        {
            //throw new NotImplementedException();
            bool isFound = false;
            foreach (string head in HeaderString)
            {
                if (action.Script.StartsWith(head))
                {
                    isFound = true;
                    break;
                }
            }

            if (isFound)
            {
                var target = ExtractSelector(action);

                //waitForElement({id:'usernamebox'},45)
                //{id:'inputEmail1'}, 'demouser@blastasia.com'
                //var split = target.Split(new string[] { ")." }, StringSplitOptions.None);
                var split = target.Split(',');

                var sqr = split[0];
                if (sqr.StartsWith("{"))
                {
                    var sqrContent = sqr.Substring(1, sqr.Length - 2);
                    sqrContent = sqrContent.Replace(":", "=");
                    sqrContent = sqrContent.Replace("'", "\"");


                    sqrContent = "'[" + sqrContent + "]'";

                    sqr = sqrContent;
                }


                var prefix1 = string.Format("waitForElement({0},45)", split[0]);
                //var prefix1 =string.Format("waitByElement({0})",split[0]);


                //retry({ retries: 3, maxTimeout: 3000 }).click({id:'usernamebox'}
                var prefix2 = @"retry({ retries: 3, maxTimeout: 3000 })";//string.Format("retry({ retries: 3, maxTimeout: 3000 })");

                //fillField({id:'usernamebox'}
                OriginalString = action.Script;
                var newScript = string.Empty;

                if (IsSemanticLocator(target))
                {
                    newScript = string.Format("{0}.see({1});I.{2};I.wait(1);", prefix2, target, OriginalString);

                }
                else
                {

                    newScript = string.Format("{0};I.{1}.{2};I.wait(1)", prefix1, prefix2, OriginalString);
                }

                Console.WriteLine(newScript);
                action.Script = newScript;

            }

            return action;
        }





    }
}
