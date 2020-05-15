
const Helper = codeceptjs.helper;
const puppeteer = require('puppeteer');
//const iPhone = puppeteer.devices['iPhone 6'];

const fs = require('fs');
const util = require('util');


class MyPuppet extends Helper {
    // add custom methods here
    // If you need to access other helpers
    // use: this.helpers['helperName']

    async scrollToElement(thisSelector) {
        const { page } = this.helpers.Puppeteer;

        await page.$eval(thisSelector, (el) => el.scrollIntoViewIfNeeded(true));
    }

    async waitByPuppet(thisSelector, timeOut) {
        const { page } = this.helpers.Puppeteer;
        //await page.emulate(iPhone);
        await page.waitForSelector(thisSelector);//then(() => console.log('waitByPuppet done');
        //return page.pdf({path: 'page.pdf'});
    }

    async waitByElement(thisSelector, timeOut) {
        const { page } = this.helpers.Puppeteer;
        //await page.emulate(iPhone);
        await page.waitForSelector(thisSelector);//then(() => console.log('waitByPuppet done');
        //return page.pdf({path: 'page.pdf'});
    }

    async waitByXPath(thisSelector, timeOut) {
        const { page } = this.helpers.Puppeteer;
        //await page.emulate(iPhone);

        try {
            await page.waitForXPath(thisSelector);//then(() => console.log('waitByPuppet done');
        } catch (e) {
            //console.log(e)
        }
        //return page.pdf({path: 'page.pdf'});
    }

    async selectNewWindow() {
        // targetCreated event listener in test.bootstrap.js activated and sets global.pages to all open windows in instance
        // new page/popup is last item in global.pages array

        var browser = this.helpers['Puppeteer'].browser;
        //let popup = browser.pages[browser.pages.length - 1];
        let popup = browser.pages[1];
        //await this.helpers['Puppeteer']._setPage(popup);

        // await popup.url(); // Get the url of the current page

        //const { page } = this.helpers.Puppeteer;
        //await page.waitForNavigation();

        //const { browser } = this.helpers.Puppeteer;
        // await browser.pages(); // List of pages in the browser
    }

    //posXY = "123###456"
    async mouseClick(posXY) {
        const { page } = this.helpers.Puppeteer;

        var coor = posXY.split("###");
        var splitX = coor[0];

        console.log("splitx=" + splitX);

        // await page.evaluate( function(){
        //   alert("splitx=" + coor[0]);
        // } );

        var splitY = coor[1];
        console.log("splity=" + coor[1]);

        try {
            await page.mouse.click(parseFloat(splitX), parseFloat(splitY), { delay: 1000 });
        } catch (e) {
            console.log(e);
        }
        //return page.pdf({path: 'page.pdf'});
    }

    async mouseClickXYToGrabValue(posXY) {
        const { page } = this.helpers.Puppeteer;

        var coor = posXY.split("###");
        var splitX = coor[0];

        console.log("splitx=" + splitX);
        // await page.evaluate( function(){
        //   alert("splitx=" + coor[0]);
        // } );

        var splitY = coor[1];
        console.log("splity=" + coor[1]);

        try {
            await page.mouse.click(parseFloat(splitX), parseFloat(splitY), { delay: 1000 });
            // await page.mouse.click(parseFloat(splitX), parseFloat(splitY), { delay: 1000 });

            // var element;
            // var innerText;
            var X = parseFloat(splitX);
            var Y = parseFloat(splitY);

            var myGrab = await page.evaluate(({ X, Y }) => {
                var element = document.elementFromPoint(X, Y);
                var innerText = element.innerText;
                return innerText;
            }, { X, Y });

            await this.WriteMyGrabValue(myGrab);

            console.log('MyGrabValue = ' + myGrab);
            //console.log(await page.evaluate(() => {
            //    var element = document.elementFromPoint(parseFloat(splitX), parseFloat(splitY));
            //    var innerText = element.innerText;

            //    WriteMyGrabValue(innerText);
            //    return innerText;
            //}));
            return myGrab;
        } catch (e) {
            console.log(e);
        }
        //return page.pdf({path: 'page.pdf'});
    }

    async mouseClickXpathToGrabValue(xpath) {
        const { page } = this.helpers.Puppeteer;

        try {
            // let el = await page.xpath(xpath);
            //let el = await page.$x(xpath);
            var selector = xpath;//"'" + xpath +"'";
            console.log(selector);
            var xpathData = await page.$x(selector);
            var xpathTextContent = await xpathData[0].getProperty('textContent')
            var text = await xpathTextContent.jsonValue();
            var myGrab = text;//el.innerText;         
            // await this.WriteMyGrabValue(myGrab);
            console.log('MyGrabValue = ' + myGrab);

            var grabPath = './output/endpoint/MyGrabValue.txt';

            try {
                // delete file named 'MyGrabValue.txt'
                fs.unlink(grabPath, function (err) {
                    if (err) console.log(err);
                    // if no error, file has been deleted successfully
                    console.log('File deleted!');
                });
            } catch (e) {
                console.log(e);
            }

            fs.writeFile(grabPath, myGrab, function (err) {
                if (err)
                    return console.log(err);
                console.log('Wrote MyGrabValue.txt, just check it');
                //ROOT_APP_PATH = fs.realpathSync('.');
                console.log(grabPath);
            });
            //  return this.MyGrabValue;
            return myGrab;
        } catch (e) {
            console.log(e);
        }
        //return page.pdf({path: 'page.pdf'});
    }

    async WriteMyGrabValue(newValue) {
        //var grabPath = '##EndPointFolder##\\MyGrabValue.txt';
        var grabPath = './output/endpoint/MyGrabValue.txt';
        var resultPath = './output/endpoint/result.txt';

        try {
            //const content = await readFile('./output/MyGrabValue.txt', 'utf8');
            const content = newValue;

            // delete file named 'MyGrabValue.txt'
            fs.unlink(grabPath, function (err) {
                if (err) throw err;
                // if no error, file has been deleted successfully
                console.log('WriteMyGrabValue: File deleted!');
            });
        } catch (e) {
            console.error(e);
        }

        fs.writeFile(grabPath, newValue, function (err) {
            if (err)
                return console.log(err);
            // this.say('write by puppeteer MyGrabValue.txt =' + content);
            console.log('WriteMyGrabValue: ReWrote by puppeteer MyGrabValue.txt =' + newValue);
            //ROOT_APP_PATH = fs.realpathSync('.');
            console.log('WriteMyGrabValue: ' + grabPath);
        });

        fs.writeFile(resultPath, newValue, function (err) {

            if (err)
                return console.log(err);
            console.log('WriteMyGrabValue: ReWrote result.txt, just check it');
            //ROOT_APP_PATH = fs.realpathSync('.');
            console.log(grabPath);
        });

        return newValue;
    }

    async sendCharacter(msgInput) {
        // targetCreated event listener in test.bootstrap.js activated and sets global.pages to all open windows in instance
        // new page/popup is last item in global.pages array

        //var browser = this.helpers['Puppeteer'].browser;
        const { page } = this.helpers.Puppeteer;
        await page.keyboard.type(msgInput);

        //await page.waitForNavigation();
    }
}

module.exports = MyPuppet;
