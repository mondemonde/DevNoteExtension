using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;
using PuppetSupportLib.Helpers;
using System.Globalization;
using CodeceptSupport.Mod;
using LogApplication.Common.Config;

namespace CodeceptSupport
{
    public class Interpreter : KatalonInterpreter
    {



        public List<CodeceptAction> MyActions { get; set; }


        public override StringBuilder ReadXmlFile(string fullFileName = "")
        {

            try
            {
                //Load default??
                #region---if null file found...


                Console.WriteLine("HACK-TEST -Convert");

                if (string.IsNullOrEmpty(fullFileName))
                {
                    fullFileName = DefaultSourceFolder + "test.xml";
                }


                #endregion //////////////END TEST

                string xml = File.ReadAllText(fullFileName);
                var catalog1 = xml.ParseXML<TestCase>();

                string myScript = string.Empty;
                //get first
                var sel = catalog1;//.FirstOrDefault();

                MyActions = new List<CodeceptAction>();
                var length = sel.selenese.Count();
                for (int i = 0; i < length; i++)
                {
                    var t = sel.selenese[i];
                    Console.WriteLine(string.Format("{0}  {1}  {2}", t.command, t.target, t.value));

                    var script = Codecept.Script(t, this);

                    myScript += script;

                    //create codeceptActions
                    CodeceptAction action = new CodeceptAction
                    {
                        target = t.target,
                        command = t.command,
                        value = t.value,
                        Script = script.Trim(),
                        OrderNo = i
                    };
                    MyActions.Add(action);

                }//end for           

                //LogApplication.Agent.LogInfo(myScript);
                Console.WriteLine(Environment.NewLine + myScript);
                StringBuilder result = new StringBuilder();
                return result;
            }
            catch (Exception err)
            {
                LogApplication.Agent.LogError(err);
               // throw;
            }

            //error or null
            return new StringBuilder();

          
        }

        [Obsolete]
        public  StringBuilder ReadJsonTestFile(string fullFileName = "")
        {
            MyActions = new List<CodeceptAction>();
            string json = File.ReadAllText(fullFileName);

            string[] split = json.Split(new string[] { "Scenario(" }, StringSplitOptions.None);
            var target = split[1];

            string[] splitByLine = target.Split('\n');
            var length = splitByLine.Count();

            string myScript = string.Empty;

            for (int i = 0; i < length; i++)
            {
                string line = splitByLine[i].Trim();
                if (line.StartsWith("I."))
                {

                    var myLine = line.Substring(2);
                    myScript += myLine;
                    
                    //create codeceptActions
                    CodeceptAction action = new CodeceptAction
                    {                       
                        Script = myLine.Trim(),
                        OrderNo = i
                    };
                    MyActions.Add(action);
                }
                else if (line.Trim() == "});")
                {
                    break;
                }
            }        

            //LogApplication.Agent.LogInfo(myScript);
            Console.WriteLine(Environment.NewLine + myScript);
            StringBuilder result = new StringBuilder(myScript);
            return result;
        }

        public StringBuilder InsertVariables(string fullFileName , Dictionary<string,string> variables)
        {
            string json = File.ReadAllText(fullFileName);

            string[] split = json.Split(new string[] { "Scenario(" }, StringSplitOptions.None);
            var target = split[1];

            string[] splitByLine = target.Split('\n');
            var length = splitByLine.Count();

            string myScript = string.Empty;

            for (int i = 0; i < length; i++)
            {
                string line = splitByLine[i].Trim();
                if (line.StartsWith("I."))
                {

                    var myLine = line;//;.Substring(2);

                    //create psuedo codeceptActions dont save this
                    CodeceptAction thisAction = new CodeceptAction
                    {
                        Script = myLine.Trim(),
                        OrderNo = i
                    };

                    //cross check database variables ex script delimitter:  'var input_1_1 ='
                    foreach(var dic in variables)
                    {
                        //TIP: SPLIT
                        string delimitter = string.Format("var {0} =", dic.Key);
                        string[] splitScript = thisAction.Script.Split(new string[] { delimitter }, StringSplitOptions.None);

                        if (splitScript.Length > 1)
                        {

                            var leftPart = splitScript[1];

                            //change value .. " 'demouser@blastasia.com';"    I.waitForElement('[id="inputEmail1"]',45);I.retry({ retries: 3, maxTimeout: 3000 }).fillField({id:'inputEmail1'}, input_1_1);I.wait(1);'
                            //string[] splitScript2 = leftPart.Split(';');
                            var leftPart2 = leftPart.Substring(0, leftPart.IndexOf(';')); //splitScript2[1];//" 'demouser@blastasia.com'"

                            var delimitter2 = delimitter + leftPart2;//var input_1_1 = 'demouser@blastasia.com'

                            string[] splitScript2 = thisAction.Script.Split(new string[] { delimitter2 }, StringSplitOptions.None);

                            //var origScript = splitScript2[0] + delimitter2 + splitScript2[1];
                            var newExpresson =  string.Format("var {0} = '{1}'", dic.Key,dic.Value);

                            var newScript = splitScript2[0] + newExpresson + splitScript2[1];

                            myLine = newScript;
                        }


                    }
                    myScript += myLine;

                }
                else if (line.Trim() == "});")
                {
                    break;
                }
            }

            //reinsert from template..
            ConfigManager config = new ConfigManager();
            var codeCeptConfigPath = config.GetValue("CodeceptTestTemplate");
            var codeCeptTestTemplate = File.ReadAllText(codeCeptConfigPath);
            codeCeptTestTemplate = codeCeptTestTemplate.Replace("##steps##", myScript);         

            //LogApplication.Agent.LogInfo(myScript);
            Console.WriteLine(Environment.NewLine + myScript);
            StringBuilder result = new StringBuilder(codeCeptTestTemplate);

            return result;
        }





        #region Selector 
        public override string FormatSelector(string target)
        {
            var content = target.ToString();



            //I.clickLink('Logout', '#nav')
            if (content.StartsWith("link="))
                return FormatAsLink(target);

            else if (content.StartsWith("xpath="))
                return FormatAsXpath(target);

            else if (content.StartsWith("//"))
                return BaiTextFilterClassLibrary.Helper.XpathHandleQuotes(target);

            else
            {

                List<string> elFilters = new List<string>() { "id=", "id =", "name=" };
                foreach (string filter in elFilters)
                {
                    if (content.Contains(filter))
                    {
                        return FormatAsJson(target);
                       // return FormatAsSquareBracket(target);

                    }
                }
            }




            return "'" + target + "'";
        }

       
        public override string FormatValue(string target)
        {
            string result = target;
            var delimiter = "${KEY_";

            if (target.StartsWith(delimiter))
            {
                result = target.Replace(delimiter, "");
                result = result.Replace("}", "");

                // Creates a TextInfo based on the "en-US" culture.
                //TextInfo txtInfo = new CultureInfo("en-US", true).TextInfo;
                result = BaiTextFilterClassLibrary.Helper.ToTitleCase(result);

            }

            return result;
        }

        private string FormatAsJson(string target)
        {
            //throw new NotImplementedException();
            var split = target.Split('=');
            if (split.Length > 1)
            {
                var attr = split[0].Trim();
                var value = string.Format("'{0}'", split[1]);
                var result = "{" + attr + ":" + value + "}";
                return result;
            }
            else
                return target;
        }

        private string FormatAsSquareBracket(string target)
        {
            //throw new NotImplementedException();
            var split = target.Split('=');
            if (split.Length > 1)
            {
                var attr = split[0].Trim();
                var value = string.Format("\"{0}\"", split[1]);
                var result = "[" + attr + "=" + value + "]";
                return result;
            }
            else
                return target;
        }

        private string FormatAsLink(string target)
        {
            //throw new NotImplementedException();
            var split = target.Split('=');
            if (split.Length > 1)
            {
                //I.clickLink('Logout', '#nav')
                //var attr = split[0].Trim();

                //link = Puppeteer
                var value = string.Format("'{0}'", split[1]);
                //'Puppeteer'
                return value;
            }
            else
                return target;
        }

        private string FormatAsXpath(string target)
        {
            // I.seeElement({ xpath: '//div[@class=user]'});
            string[] delimeter = new string[] { "xpath=" };
            string[] split = target.Split(delimeter, StringSplitOptions.None);

            if (split.Length > 1)
            {
                var attr = "xpath";
                var value = string.Format("\"{0}\"", split[1]);

                value = value.Replace("=\"", "='");
                value = value.Replace("\"]", "']");

                var result = "{" + attr + ":" + value + "}";


                return result;
            }
            else
                return target;

        }


        #endregion


    }




    public static class InterpreterExtensionMethods
    {
        public static Interpreter Mod<T>(this Interpreter it,T modifier) where T:BaseModifier
        {

            List<CodeceptAction> newActions = new List<CodeceptAction>() ;
            foreach (CodeceptAction action in it.MyActions)
            {
                var  newAction = modifier.Modify(action);
                newActions.Add(newAction);
                //return productViewModels;
            }
            it.MyActions = newActions;

            return it;

        }
    }
}