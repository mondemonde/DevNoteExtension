using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PuppetSupportLib;
using PuppetSupportLib.Katalon;
using PuppetSupportLib.WebAction;

namespace CodeceptSupport
{
    public class SelectWindow : BaseAction
    {

        public SelectWindow(TestCaseSelenese katalonxml) : base(katalonxml)
        {

        }

        public override TestCaseSelenese Map(object customAction)
        {
            //throw new NotImplementedException();
            var act = (TestCaseSelenese)customAction;
            //do convettion here..
            //..
            //.


            return act;
        }

        
        public override string Script(IInterpreter interpreter)
        {
            //PROTRACTOR
            //const handles_3 = await browser.getAllWindowHandles();
            //await browser.switchTo().window(handles_3[handles_3.length - 1]);

            /*CODECEPT
                         
            const handleBeforePopup = await I.grabCurrentWindowHandle();
            const urlBeforePopup = await I.grabCurrentUrl();
            const allHandlesBeforePopup = await I.grabAllWindowHandles();
            await I.switchToWindow(allHandlesAfterPopup[1]);
            const urlAfterPopup = await I.grabCurrentUrl();
            assert.equal(urlAfterPopup, 'https://www.w3schools.com/', 'Expected URL: Popup');

            assert.equal(handleBeforePopup, allHandlesAfterPopup[0], 'Expected Window: Main Window');
            await I.switchToWindow(handleBeforePopup);
            const currentURL = await I.grabCurrentUrl();
            assert.equal(currentURL, urlBeforePopup, 'Expected URL: Main URL');

            */

            //await page.click('.container > #mvcforum-nav > .nav > li > .auto-logon')
           //var script = string.Format("say('New Window');const handle_ = await I.grabAllWindowHandles(); await I.switchToWindow(handle_[1]);");
           var script = string.Format("say('New Window');I.switchToNextTab();");
              



            script = script + Environment.NewLine;

            return script;
        }
    }
}
