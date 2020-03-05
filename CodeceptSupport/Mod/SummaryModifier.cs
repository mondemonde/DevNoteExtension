using BaiTextFilterClassLibrary;
using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport.Mod
{
    public class SummaryModifier : BaseModifier
    {

        //public ClickModifier(IInterpreter it):base(it)
        //{
        //    HeaderString = new List<string> { "click(" , "clickLink(" };
        //}
        public SummaryModifier() : base()
        {
            HeaderString = new List<string> { Keywords.declareVariable };
            ListOfVariables = new List<string>();
        }


        public List<string> ListOfVariables { get; set; }

       // public int inputCount { get; set; }
        public override CodeceptAction Modify(CodeceptAction action)
        {
            throw new NotImplementedException();
        }

        public static  List<CodeceptAction> AddSummary(Interpreter it,List<string> variables)
        {

            var actions = it.MyActions;



            return it.MyActions;
        }


    }
}
