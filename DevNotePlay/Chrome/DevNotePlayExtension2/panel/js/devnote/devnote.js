/*
 * Copyright 2020 rgalvez
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 *
 */

//_STEP#4 UI add chrome.runtime.onMessage.addListener()
chrome.runtime.onMessage.addListener(
    function(request, sender, sendResponse) {
        console.log(sender.tab ?
            "from a content script:" + sender.tab.url :
            "from the extension");
        //_STEP#50 close katalon
        if (request.greeting === "close_Katalon") {
            close_Katalon();
            sendResponse({ farewell: request.greeting });
        }
        else if (request.greeting === "load_Latest") {
            load_Latest();
            // let thisLoad =  async function() {
            //     debugger;
            //    loadLatestRecord(); 
            //  };

            // thisLoad().then(sendResponse({ farewell: request.greeting }));
        }
        else if (request.greeting === "play_Suite") {
            play_Suite();
            sendResponse({ farewell: request.greeting });
        }
    }
);

function close_Katalon() {
    //document.getElementById('close-all-testSuites').click();
    $("#close-all-testSuites").trigger("click");
    closeConfirm(false);

    setTimeout(function(){
        chrome.windows.getAll({}, function(windows){
            for(var i = 0; i < windows.length; i++)
            chrome.windows.remove(windows[i].id);
        });
        window.close();
    }, 1000);
}

//_STEP#23 load_Latest method
function load_Latest() {  
   $("#load-testSuite-hidden").trigger("click");  
}

//_STEP#23 load_Latest method
function play_Suite() {
    // readSuiteFromString();
    $("#playSuite").trigger("click");
}