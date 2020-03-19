exports.config = {
    tests: './*_test.js',
    output: '/../output',
    helpers: {
        Puppeteer: {
            url: 'http://localhost',
            show: true,
            restart: true,
            // windowSize: "1280x960",
            windowSize: "1366x960",//await page.setViewport({ width: 1366, height: 768});
            waitForNavigation: "networkidle0"
        },

        MyPuppet: {
            require: "./mypuppet_helper.js", // path to module
            defaultHost: "https://google.com" // custom config param
        }


    },
    include: {
        I: './steps_file.js'
    },
    bootstrap: null,
    mocha: {},
    name: 'Project2',
    plugins: {
        screenshotOnFail: {
            enabled: true
        },
        autoDelay: {
            enabled: true,
            delayBefore: 400,
            delayAfter: 900
        },
        retryFailedStep: {
            enabled: true,
            retries: 2
        }
    }
}