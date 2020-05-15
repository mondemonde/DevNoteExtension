// eslint-disable-next-line
/// <reference path="./steps.d.ts"/>
// in this file you can append custom step methods to 'I' object

const fs = require('fs');
const util = require('util');


module.exports = function inject() {
    return actor({
        // Define custom steps here, use 'this' to access default methods of I.
        // It is recommended to place a general 'login' function here.

        // login(email, password) {
        //   within({ xpath: "//div[@class='auth-form-body mt-3']" }, () => {
        //     this.fillField('#login_field', email);
        //     this.fillField('#password', password);
        //     this.click('Sign in');
        //   });
        // },
        async grabAsync(element, isMultiGrab) {
            this.MyGrabElement = element;
            this.say(this.MyGrabElement);
            //this.MyGrabValue = this.MyGrabElement.content;
            this.MyGrabValue = await this.grabTextFrom(element);
            this.say(this.MyGrabValue);
            console.log('grabAsync: element', element);
            console.log('grabAsync: grabValue', this.MyGrabValue);
            //this.click(element);    
            //this.helpers['Puppeteer'].click(element);
            //write config file
            //fs.writeFile('./output/MyGrabValue.txt', this.MyGrabValue, function (err) {
            //var grabPath = '##EndPointFolder##\\MyGrabValue.txt';
            var grabPath = './output/endpoint/MyGrabValue.txt';

            try {
                // delete file named 'MyGrabValue.txt'
                fs.unlink(grabPath, function (err) {
                    if (err) console.log(err);
                    // if no error, file has been deleted successfully
                    console.log('grabAsync: File deleted!');
                });
            }
            catch (e) {
                console.log(e);
            }

            if (Array.isArray(this.MyGrabValue)) {
                console.log('grabAsync: Value is array.');
                this.MyGrabValue = this.MyGrabValue[0];
                console.log('grabAsync: Getting first value...');
            }

            //if (!isMultiGrab) {
            //    this.MyGrabValue = this.MyGrabValue[0];
            //}

            fs.writeFile(grabPath, this.MyGrabValue, function (err) {
                if (err)
                    return console.log(err);
                console.log('grabAsync: Wrote MyGrabValue.txt, just check it');
                //ROOT_APP_PATH = fs.realpathSync('.');
                console.log('grabAsync: ' + grabPath);
            });

            return this.MyGrabValue;
        },
        MyGrabElement: '',
        MyGrabValue: 'HELLO',
        MyVar1: '',

        async getMyGrabValue() {
            //var grabPath = '##EndPointFolder##\\MyGrabValue.txt';
            var grabPath = './output/endpoint/MyGrabValue.txt';

            let readFile = util.promisify(fs.readFile);

            try {
                //const content = await readFile('./output/MyGrabValue.txt', 'utf8');
                const content = await readFile(grabPath, 'utf8');

                this.MyGrabValue = content;
                this.say('read MyGrabValue.txt =' + content);
                //// delete file named 'MyGrabValue.txt'
                //fs.unlink(grabPath, function (err) {
                //    if (err) throw err;
                //    // if no error, file has been deleted successfully
                //    console.log('File deleted!');
                //});               
            }
            catch (e) {
                console.error(e);
            }
            //fs.writeFile(grabPath, this.MyGrabValue, function (err) {
            //    if (err)
            //        return console.log(err);
            //    console.log('ReWrote MyGrabValue.txt, just check it');
            //    //ROOT_APP_PATH = fs.realpathSync('.');
            //    console.log(grabPath);
            //});
            return this.MyGrabValue;
        },
        async CloseStepRun() {
            //var grabPath = './output/endpoint/MyGrabValue.txt';
            //let readFile = util.promisify(fs.readFile);

            //try {
            //    const content = await readFile(grabPath, 'utf8');
            //    this.MyGrabValue = content;
            //    this.say('read MyGrabValue.txt =' + content);

            //    fs.unlink(grabPath, function (err) {
            //        if (err) throw err;
            //        // if no error, file has been deleted successfully
            //        console.log('File deleted!');
            //    });
            //} catch (e) {
            //    console.error(e);
            //}
            //fs.writeFile(grabPath, this.MyGrabValue, function (err) {
            //    if (err)
            //        return console.log(err);
            //    console.log('ReWrote MyGrabValue.txt, just check it');
            //    //ROOT_APP_PATH = fs.realpathSync('.');
            //    console.log(grabPath);
            //});

            //return this.MyGrabValue;

            //var grabPath = '##EndPointFolder##\\MyGrabValue.txt';
            var grabPath = './output/endpoint/MyGrabValue.txt';
            var resultPath = './output/endpoint/result.txt';

            let readFile = util.promisify(fs.readFile);

            try {
                //const content = await readFile('./output/MyGrabValue.txt', 'utf8');
                const content = await readFile(grabPath, 'utf8');

                this.MyGrabValue = content;
                this.say('read MyGrabValue.txt =' + content);

                // delete file named 'MyGrabValue.txt'
                fs.unlink(grabPath, function (err) {
                    if (err) throw err;
                    // if no error, file has been deleted successfully
                    console.log('File deleted!');
                });
            } catch (e) {
                console.error(e);
            }

            fs.writeFile(grabPath, this.MyGrabValue, function (err) {

                if (err)
                    return console.log(err);
                console.log('ReWrote MyGrabValue.txt, just check it');
                //ROOT_APP_PATH = fs.realpathSync('.');
                console.log(grabPath);
            });

            fs.writeFile(resultPath, this.MyGrabValue, function (err) {

                if (err)
                    return console.log(err);
                console.log('ReWrote result.txt, just check it');
                //ROOT_APP_PATH = fs.realpathSync('.');
                console.log(grabPath);
            });

            return this.MyGrabValue;
        },
        async CloseAndCreateResultFile() {
            //var grabPath = '##EndPointFolder##\\MyGrabValue.txt';
            var grabPath = './output/endpoint/MyGrabValue.txt';
            var resultPath = './output/endpoint/result.txt';

            let readFile = util.promisify(fs.readFile);

            try {
                const content = await readFile(grabPath, 'utf8');
                this.MyGrabValue = content;
                this.say('read MyGrabValue.txt =' + content);

                // delete file named 'MyGrabValue.txt'
                fs.unlink(resultPath, function (err) {
                    if (err) console.log(err);
                    // if no error, file has been deleted successfully
                    console.log('File deleted!');
                });
            } catch (e) {
                console.error(e);
            }

            fs.writeFile(resultPath, this.MyGrabValue, function (err) {
                if (err)
                    return console.log(err);
                console.log('ReWrote result.txt, just check it');
                //ROOT_APP_PATH = fs.realpathSync('.');
                console.log(resultPath);
            });

            return this.MyGrabValue;
        }
    });
};
