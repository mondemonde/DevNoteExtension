using PuppetSupportLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeceptSupport.Mod
{
    public class FinalModifier : BaseModifier
    {

        //public ClickModifier(IInterpreter it):base(it)
        //{
        //    HeaderString = new List<string> { "click(" , "clickLink(" };
        //}
        public FinalModifier() : base()
        {
            //HeaderString = new List<string> { "click(", "clickLink(", "fillField(" };
            HeaderString = new List<string>();

        }

        public override CodeceptAction Modify(CodeceptAction action)
        {

            if (string.IsNullOrEmpty(action.Script))
                return action;


            action.Script = action.Script.Replace(";;",";");

            if (action.Script.Last() != ';')
                action.Script = action.Script + ";";

            return action;

        }

       



    }
}
