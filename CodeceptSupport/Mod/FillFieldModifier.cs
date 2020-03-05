using BaiTextFilterClassLibrary;
using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport.Mod
{
    public class FillFieldModifier : BaseModifier
    {

        //public ClickModifier(IInterpreter it):base(it)
        //{
        //    HeaderString = new List<string> { "click(" , "clickLink(" };
        //}
        public FillFieldModifier() : base()
        {
            HeaderString = new List<string> { "fillField(" };
        }


        public int inputCount { get; set; }
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
                inputCount += 1;
                var target =ExtractSelector(action);

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


                var prefix1 =string.Format("waitForElement({0},45)",sqr);

                //retry({ retries: 3, maxTimeout: 3000 }).click({id:'usernamebox'}
                var prefix2 = @"retry({ retries: 3, maxTimeout: 3000 })";//string.Format("retry({ retries: 3, maxTimeout: 3000 })");

                //fillField({id:'usernamebox'}
                OriginalString = action.Script;
                var newScript = string.Empty;                        

                if(IsSemanticLocator(target))
                {
                    newScript = string.Format("{0}.see({1});I.{2};I.wait(1);", prefix2, target,OriginalString);

                }
                else
                {

                      newScript = string.Format("{0};I.{1}.{2};I.wait(1)", prefix1,prefix2, OriginalString);
                }

                Console.WriteLine(newScript);

                //add variable prefix here
                //waitForElement({id:'inputEmail1'},45);I.retry({ retries: 3, maxTimeout: 3000 })
                //.fillField
                //({id:'inputEmail1'}, 'demouser@blastasia.com')
                //; I.wait(1)

                var splitFillField = newScript.Split(new string[] { ".fillField" }, StringSplitOptions.None);
                var fillFieldLast = splitFillField.Last();

                var splitWait = fillFieldLast.Split(';');
                var fillFieldParam = splitWait.First();//({id:'inputEmail1'}, 'demouser@blastasia.com')

                var splitFillFieldParam = fillFieldParam.Split(new string[] { ", '" }, StringSplitOptions.None);
                if(splitFillFieldParam.Count()==1)
                {
                    splitFillFieldParam = fillFieldParam.Split(new string[] { ",'" }, StringSplitOptions.None);
                }

                //demouser@blastasia.com')
                var inputVal = splitFillFieldParam.Last();
                var splitMore = inputVal.Split(new string[] { "'" }, StringSplitOptions.None);

                //demouser@blastasia.com
                inputVal = splitMore.First();


                var inputVar ="input_" + inputCount.ToString() + "_";
                newScript = newScript.Replace("'"+ inputVal +"'", inputVar );

                //step# 9 DECLARE variable interpreter
                var variablePrefix = string.Format("{0}{1} = '{2}';I.",Keywords.declareVariable, inputVar, inputVal);

                newScript = variablePrefix + newScript;
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
