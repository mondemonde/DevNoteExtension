using PuppetSupportLib.Katalon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuppetSupportLib.WebAction
{
    //same as IConverter
    public abstract class BaseAction
    {
        public BaseAction(object customAction)
        {
            _webAction = customAction;
        }

        private object _webAction;
        private TestCaseSelenese _myAction;

        public TestCaseSelenese MyAction
        {
            get
            {
                if (_myAction == null)
                    _myAction = Map(_webAction);
                return _myAction;
            }

           // set => _myAction = value;
        }
        public abstract string Script( IInterpreter interpreter);
        public abstract TestCaseSelenese Map(object customAction);

    }

}
