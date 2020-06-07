using CodeceptSupport.Mod;
using Common;
using LogApplication.Common.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport
{
   public  class ScriptReader
    {
        public List<CodeceptAction> MyActions { get; set; }

        public string MyScript { get; set; }

        public string BareCodes { get; set; }

        public void ReadXML(string file)
        {


            MyScript = string.Empty;
            MyActions = new List<CodeceptAction>();
            BareCodes = string.Empty;

            //var file = @"D:\_MY_PROJECTS\Mond\AIFS_Manager\DevNoteCmd\Katalon\Xamun.xml";// @"D:\_MY_PROJECTS\Mond\AIFS_Manager\CodeceptSupport\Katalon\test.xml";
            //run series of commands
            Interpreter it = new Interpreter();

            //step# 80 _Entry CONVERSION xml to codecept
            //_STEP_.Player _Entry CONVERSION xml to codecept
            it.ReadXmlFile(file);
            if (it.MyActions == null)
                return;

            //STEP.CodeCept #80 ACTIONS modifier
            //step# 81 mods..modify actions
            ClickModifier clickExt = new ClickModifier();
            it.Mod<ClickModifier>(clickExt);

            SendKeyModifier keyExt = new SendKeyModifier();
            it.Mod<SendKeyModifier>(keyExt);


            //step# 82 Declare VARIABLES
            FillFieldModifier fillFieldExt = new FillFieldModifier();
            it.Mod<FillFieldModifier>(fillFieldExt);

            //step# 83 identify Variables 
            VariableModifier variableList = new VariableModifier();
            it.Mod<VariableModifier>(variableList);


            //step# 83 assign Variables 
            AssignModifier variableExt = new AssignModifier();
            it.Mod<AssignModifier>(variableExt);

            //step# 84 finalize
            FinalModifier finalExt = new FinalModifier();
            it.Mod<FinalModifier>(finalExt);

            //MyActions = it.MyActions;
            //add summary
            MyActions = SummaryModifier.AddSummary(it, variableList.ListOfVariables);





            //save
            //var list = actionSource.List;
            var length = MyActions.Count;
            List<CodeceptAction> myList = new List<CodeceptAction>();

            string codes = string.Empty;

            for (int i = 0; i < length; i++)
            {
                CodeceptAction a = (CodeceptAction)MyActions[i];
                //myList.Add(a);
                if (string.IsNullOrEmpty(a.Script))
                    continue;

                var code = a.Script.Trim();

                while (code.Last() == ';')
                {
                    code = code.Substring(0, code.Length - 1);
                }

                if (code.StartsWith("say('step#"))
                    codes = codes + string.Format("I.{0};\n", code);
                else
                    codes = codes + string.Format("I.say('step#{0}');I.{1};\n", a.OrderNo.ToString(), code);


            }

            BareCodes = codes;

            //step# _8.4 config.GetValue("CodeceptTestTemplate");
            ConfigManager config = new ConfigManager();
            var codeCeptConfigPath = FileEndPointManager.MyCodeceptTestTemplate;//config.GetValue("CodeceptTestTemplate");
           


            var codeCeptTestTemplate = File.ReadAllText(codeCeptConfigPath);
            codeCeptTestTemplate = codeCeptTestTemplate.Replace("##steps##", codes);

            MyScript = codeCeptTestTemplate;

        }

       
    }
}
