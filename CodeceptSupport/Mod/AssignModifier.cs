using BaiTextFilterClassLibrary;
using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport.Mod
{
    public class AssignModifier : BaseModifier
    {

        //public ClickModifier(IInterpreter it):base(it)
        //{
        //    HeaderString = new List<string> { "click(" , "clickLink(" };
        //}
        public AssignModifier() : base()
        {
            HeaderString = new List<string> { Keywords.declareVariable ,Keywords.useVariable};
            //ListOfVariables = new List<string>();
            Parameters = new Dictionary<string, string>();
        }


        //public List<string> ListOfVariables { get; set; }
        public Dictionary<string, string> Parameters {get;set;}

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

                foreach (var keyWord in HeaderString)
                {

                    var expressions = action.Script.Split(new string[] {keyWord}, StringSplitOptions.None);
                    //I.say('DECLARE');var
                    //TIP: we only allow one varible declare per action line OR we only covert the first var
                    if (expressions.Length > 1)
                    {
                        //X='123';I.say('END_DECLARE')";I.fillField({id:'usernamebox'}
                        var expression = expressions[1].Split(';').First();

                        //x ='123'
                        //x
                        var xName = expression.Split('=').First().Trim();
                        var xValue = expression.Split('=').Last();
                        //Parameters[xName]

                        if (Parameters.ContainsKey(xName))
                        {
                            //do substitution
                           action.Script =  action.Script.Replace(xValue, Parameters[xName]);
                        }



                    }

                }

              

            }

            return action;
        }

      
    }
}
