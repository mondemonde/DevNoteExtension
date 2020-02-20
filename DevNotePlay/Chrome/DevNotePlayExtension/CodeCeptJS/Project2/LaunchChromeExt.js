
const puppeteer = require('puppeteer');
const path = require('path')


//D:\_Codecepts2\KatExt
//const extensionPath = '/_Codecepts2/KatExt'; // For instance, 'dist'

//xTODO ADD TO CONFIG KATALON EXT folder
//D:\_Codecepts2\Katalon-extention-modified
const extensionPath = '/_Codecepts2/Katalon-extention-modified'; // For instance, 'dist'



(async () => {
    //   const browser = await puppeteer.launch();
    //   const page = await browser.newPage();
    //   await page.goto('https://google.com');

    const browser = await puppeteer.launch({
        headless: false, // extension are allowed only in the head-full mode

        //  windowSize: "1280x960",
        defaultViewport: {
            width: 1280,
            height: 960
        },
        // windowSize: "1280x960",
        args: [
            `--disable-extensions-except=${extensionPath}`,
            `--load-extension=${extensionPath}`,
            '--no-sandbox', '--disable-setuid-sandbox'
           
            //`--defaultViewport={width: 1280,height: 960}`
            //'--remote-debugging-port=8088'
        ]
    });
    let pages = await browser.pages();
    //let page = pages[0];
    await pages.setViewport({ width: 960, height: 1280 });////windowSize: "1280x960",

    //const client = await page.target().createCDPSession();
    //await client.send('Network.clearBrowserCookies');
    //await client.send('Network.clearBrowserCache');

    //await page.screenshot({path: 'example.png'});
    //await browser.close();

})();