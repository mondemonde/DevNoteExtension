using BaiTextFilterClassLibrary;
using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport.Mod
{
    public class VariableModifier : BaseModifier
    {

        //public ClickModifier(IInterpreter it):base(it)
        //{
        //    HeaderString = new List<string> { "click(" , "clickLink(" };
        //}
        public VariableModifier() : base()
        {
            HeaderString = new List<string> { Keywords.declareVariable };
            ListOfVariables = new List<string>();
        }


        public List<string> ListOfVariables { get; set; }

       // public int inputCount { get; set; }
        public override CodeceptAction Modify(CodeceptAction action)
        {
            //throw new NotImplementedException();
            bool isFound = false;
            foreach(string head in HeaderString)
            {
               if(action.Script.Contains(head))
                {
                    isFound = true;
                    break;
                }
            }

           if(isFound)
            {
                //inputCount += 1;

                var expressions = action.Script.Split(new string[] { Keywords.declareVariable }, StringSplitOptions.None);
                //I.say('DECLARE');var
                //TIP: we only allow one varible declare per action line OR we only covert the first var
                if(expressions.Length>1)
                {
                    //X='123';I.say('END_DECLARE')";I.fillField({id:'usernamebox'}
                    var expression = expressions[1].Split(';').First();

                    //x ='123'
                    //x
                    var xName = expression.Split('=').First().Trim();
                    if (!ListOfVariables.Contains(xName))
                        ListOfVariables.Add(xName);
                    else
                    {
                        //remove declartion
                        //var1='123';I.say('END_DECLARE')";I.fillField({id:'usernamebox'}

                        action.Script = Keywords.useVariable + expressions[1];

                    }


                    //'123'
                    //var xValue = expression.Split('=').Last().Trim();
                }               

            }

            return action;
        }

        

    }
}
