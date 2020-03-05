using LogApplication.Common.Config;
using Newtonsoft.Json;
using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CodeceptSupport.Mod
{
    public class ClickModifier : BaseModifier
    {

        //public ClickModifier(IInterpreter it):base(it)
        //{
        //    HeaderString = new List<string> { "click(" , "clickLink(" };
        //}

        public int DefaultWait { get; set; }

        public ClickModifier() : base()
        {
            //HeaderString = new List<string> { "click(", "clickLink(", "fillField(" };
            HeaderString = new List<string> { "click(", "clickLink(","mouseClick(" };
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

            var JSONObj = new Dictionary<string, string>();

            if (isFound)
            {
                var target =ExtractSelector(action);

                //waitForElement({id:'usernamebox'},45)
                //{id:'inputEmail1'}, 'demouser@blastasia.com'
                //var split = target.Split(new string[] { ")." }, StringSplitOptions.None);
                var split = target.Split(',');

                //wait for use square barcket
                var sqr = split[0];
                var prefix1 = string.Empty;

              
               if (sqr.StartsWith("'//"))
                {


                    prefix1 = string.Format("waitByXPath({0},5);I.wait(1)", sqr);

                }
               else  if (sqr.StartsWith("{xpath:"))
                {

                    var orig = sqr;
                    sqr = target;

                    //{ xpath: "//*[@id="main - wrap"]/div[2]/div[1]/div[1]/div[2]/div[4]/div"}
                    //sqr = BaiTextFilterClassLibrary.Helper.XpathHandleQuotes(sqr); ==>  '{xpath:"//*[@id="main-wrap"]/div[2]/div[1]/div[1]/div[2]/div[4]/div"}'
                    //var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(sqr);

                    try
                    {
                        //JSONObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(sqr);
                        JSONObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(sqr);


                    }
                    catch (Exception)
                    {
                        sqr = sqr.Replace("=\"", "='");
                        sqr = sqr.Replace("\"]", "']");

                        JSONObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(sqr);

                    }

                    var xpath = JSONObj["xpath"];
                    sqr = string.Format("{0}", xpath);

                    sqr = BaiTextFilterClassLibrary.Helper.XpathHandleQuotes(sqr);

                    prefix1 = string.Format("waitByXPath({0},5);I.wait(1)", sqr);


                    #region MAYBE NOT NEEDED

                    ////{"xpath":"(//input[@value=''])[2]"}, 5
                    //string[] delimeter = new string[] { "])" };

                    //var split0 = sqr.Split(delimeter, StringSplitOptions.None);

                    ////{"xpath":"(//input[@value=''])[2]
                    //var sqr1 = split0[0];
                    //delimeter = new string[] { "//" };
                    //var split1 = sqr1.Split(delimeter, StringSplitOptions.None);

                  
                    //var sqr2 = "";

                    //if (split1.Length > 1)
                    //{
                    //    //input[@value=''])[2]
                    //    sqr2 = split1[1];
                    //    sqr = "//" + sqr2 + "]";

                    //}
                    //else
                    //{
                    //    //TIP: xpath
                    //    //{xpath:"/html/body/div[6]/div[2]/div[1]/div[1]/div[2]/div[4]/div"}
                    //    // should transform to ==>> '//html/body/div[6]/div[2]/div[1]/div[1]/div[2]/div[4]/div'

                    //   // var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(sqr);

                    //   // var xpath = JSONObj["xpath"];
                    //    sqr = string.Format("{0}", xpath);

                    //    //I.waitByXPath('//html/body/div[6]/div[2]/div[1]/div[1]/div[2]/div[4]/div',5)

                    //}


                    //sqr = BaiTextFilterClassLibrary.Helper.XpathHandleQuotes(sqr);

                    //prefix1 = string.Format("waitByXPath({0},5);I.wait(1)", sqr);

                    #endregion

                }
               else if (sqr.StartsWith("{"))
                {
                    var sqrContent = sqr.Substring(1, sqr.Length - 2);
                    sqrContent = sqrContent.Replace(":", "=");
                    sqrContent = sqrContent.Replace("'", "\"");


                    sqrContent = "'[" + sqrContent + "]'";
                    sqr = sqrContent;

                    prefix1 = string.Format("waitByElement({0})", sqr);

                }
               else
                {
                    // var prefix1 =string.Format("waitForElement({0},45)",split[0]);
                    prefix1 = string.Format("waitForElement({0},45)", sqr);
                }             

                //var prefix1 =string.Format("waitByElement({0})",split[0]);
                //retry({ retries: 3, maxTimeout: 3000 }).click({id:'usernamebox'}
                var prefix2 = @"retry({ retries: 3, maxTimeout: 3000 })";//string.Format("retry({ retries: 3, maxTimeout: 3000 })");

                //fillField({id:'usernamebox'}
                OriginalString = action.Script;
                var newScript = string.Empty;

                if (IsSemanticLocator(target))
                {
                    if (OriginalString.StartsWith("mouseClick("))//mousceclick(posxy)
                    {
                        newScript = string.Format("{0}", OriginalString); //wait is handle by click codecept

                    }
                    else //click('login');
                    {
                        //newScript = string.Format("{0}.see({1});I.{2};I.wait({3});", prefix2, target, OriginalString, DefaultWait);
                        newScript = string.Format("{0};I.wait({1});", OriginalString, DefaultWait);

                    }




                }
                else
                {
                  

                    newScript = string.Format("{0};I.{1}.{2};I.wait({3})", prefix1, prefix2, OriginalString, DefaultWait);
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
