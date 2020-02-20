
Feature('template');

Scenario('template', async (I) => {
var MyGrabValue='';


    I.say('step#0');I.amOnPage('https://alpha.quickreach.co/');I.say('step#1');I.waitByElement('[id="inputEmail1"]');I.retry({ retries: 3, maxTimeout: 3000 }).click({id:'inputEmail1'});I.wait(3);I.say('step#2');I.say('DECLARE');var input_1_1 = 'demouser@blastasia.com';I.waitForElement('[id="inputEmail1"]',45);I.retry({ retries: 3, maxTimeout: 3000 }).fillField({id:'inputEmail1'}, input_1_1);I.wait(1);I.say('step#3');I.waitByElement('[id="inputPassword"]');I.retry({ retries: 3, maxTimeout: 3000 }).click({id:'inputPassword'});I.wait(3);I.say('step#4');I.say('DECLARE');var input_2_2 = 'Pass@word1';I.waitForElement('[id="inputPassword"]',45);I.retry({ retries: 3, maxTimeout: 3000 }).fillField({id:'inputPassword'}, input_2_2);I.wait(1);I.say('step#5');I.waitByXPath('//button[@type='+"'"+'submit'+"'"+']',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click('//button[@type='+"'"+'submit'+"'"+']');I.wait(3);I.say('step#6');I.waitByXPath('//i',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click('//i');I.wait(3);
  

});

After((I) => {
  I.CloseAndCreateResultFile(); 
});
