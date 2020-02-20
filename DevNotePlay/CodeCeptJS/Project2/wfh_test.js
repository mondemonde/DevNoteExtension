
Feature('template');

Scenario('template', async (I) => {
var MyGrabValue='';


    I.say('step#0');I.amOnPage('https://app.xamun.com/');
I.say('step#1');I.waitByElement('[id="usernamebox"]');I.retry({ retries: 3, maxTimeout: 3000 }).click({id:'usernamebox'});I.wait(2);
I.say('step#2');I.say('DECLARE');var input_1_1 = 'rgalvez@blastasia.com';I.waitForElement('[id="usernamebox"]',45);I.retry({ retries: 3, maxTimeout: 3000 }).fillField({id:'usernamebox'}, input_1_1);I.wait(1);
I.say('step#3');I.say('DECLARE');var input_2_2 = 'Kc2n6Z7R';I.waitForElement('[id="userpasswordbox"]',45);I.retry({ retries: 3, maxTimeout: 3000 }).fillField({id:'userpasswordbox'}, input_2_2);I.wait(1);
I.say('step#4');I.waitByElement('[id="loginButton"]');I.retry({ retries: 3, maxTimeout: 3000 }).click({id:'loginButton'});I.wait(2);
I.say('step#5');I.click('Activity Report');I.wait(2);
I.say('step#6');I.waitByXPath('//div[3]/div[3]/div',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click('//div[3]/div[3]/div');I.wait(2);
I.say('step#7');I.mouseClick('175###174');I.wait(3);
I.say('step#8');I.waitByXPath('//div[3]/div[3]/div[2]/div[2]/div/div',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click('//div[3]/div[3]/div[2]/div[2]/div/div');I.wait(2);
I.say('step#9');I.mouseClick('455###330');I.wait(3);
I.say('step#10');I.waitByXPath('(//div[@onclick='+"'"+'javascript:hideHint(this,event);'+"'"+'])[3]',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click({xpath:"(//div[@onclick='javascript:hideHint(this,event);'])[3]"});I.wait(2);
I.say('step#11');I.say('DECLARE');var input_3_3 = 'B5 L25 Espina St. Maligaya Capitol Park Land Camarin Caloocan City';I.waitForElement('//div[2]/div[2]/input',45);I.retry({ retries: 3, maxTimeout: 3000 }).fillField('//div[2]/div[2]/input', input_3_3);I.wait(1);
I.say('step#12');I.waitByXPath('(//div[@onclick='+"'"+'javascript:hideHint(this,event);'+"'"+'])[4]',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click({xpath:"(//div[@onclick='javascript:hideHint(this,event);'])[4]"});I.wait(2);
I.say('step#13');I.say('DECLARE');var input_4_4 = '89289623';I.waitForElement('//div[3]/input',45);I.retry({ retries: 3, maxTimeout: 3000 }).fillField('//div[3]/input', input_4_4);I.wait(1);
I.say('step#14');I.waitByXPath('(//div[@onclick='+"'"+'javascript:hideHint(this,event);'+"'"+'])[5]',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click({xpath:"(//div[@onclick='javascript:hideHint(this,event);'])[5]"});I.wait(2);
I.say('step#15');I.sendCharacter('Mond Galvez');I.pressKey('Tab');
I.say('step#16');I.say('DECLARE');var input_5_5 ='2/12/2020';I.sendCharacter(input_5_5);I.pressKey('Tab');
I.say('step#17');I.say('DECLARE');var input_6_6 ='2/12/2020';I.sendCharacter(input_6_6);I.pressKey('Tab');

//I.say('step#18');I.waitByXPath('(//input[@type='+"'"+'checkbox'+"'"+'])[3]',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click({xpath:"(//input[@type='checkbox'])[3]"});I.wait(2);

I.say('step#19');I.waitByXPath('//input[@value='+"'"+''+"'"+']',5);I.wait(1);I.retry({ retries: 3, maxTimeout: 3000 }).click('//input[@value='+"'"+''+"'"+']');I.wait(2);
I.say('step#20');I.sendCharacter('2pm');I.pressKey('Tab');
I.say('step#21');I.sendCharacter('11pm');I.pressKey('Tab');
I.say('step#22');I.sendCharacter('6-7pm');I.pressKey('Tab');
I.say('step#23');I.sendCharacter('8');I.pressKey('Tab');
I.say('step#24');I.sendCharacter('not fit to commute');I.pressKey('Tab');
I.say('step#25');I.sendCharacter('Update TFS');I.pressKey('Tab');
I.say('step#26');I.say('breakpoint');
I.say('step#27');I.sendCharacter('Email/Skype');I.pressKey('Tab');
I.say('step#28');I.sendCharacter('Laptop');I.pressKey('Tab');
pause();

  

});

After((I) => {
  I.CloseAndCreateResultFile(); 
});
