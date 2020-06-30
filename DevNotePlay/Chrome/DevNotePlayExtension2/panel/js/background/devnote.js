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



    });


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

 function loadLatestRecord() {  

   toggleLoader();
    var url = "http://localhost/DevNoteFront/api/latest";
    var settings = {
        "url": url,
        "method": "GET",
        "timeout": 0
    };
    $.ajax(settings).done(function (response) {


        var date = new Date();
        console.log(date);
        deleteExistingLibrary();

        debugger;

        // var blob = new Blob([response], {
        //     type: 'text/plain'
        // });
        // readMySuite(blob);

        suiteReader(null, response);
        appendTestSuite(null, f, 'Record Library');

        toggleLoader();
    });
}

function readMySuite(f) {

    var reader = new FileReader();
    if (!f.name.includes("htm")) return;
    reader.readAsText(f);

    reader.onload = function(event) {
        setTimeout(saveData, 0);
        var test_suite = reader.result;
        // check for input file version
        // if it is not SideeX2, transforming it
        if (!checkIsVersion2(test_suite)) {
            if (test_suite.search("<table") > 0 && test_suite.search("<datalist>") < 0) {
                // TODO: write a non-blocked confirm window
                // confrim user if want to transform input file for loading it
                /* KAT-BEGIN no confirm
                let result = window.confirm("\"" + f.name + "\" is of the format of an early version of Selenium IDE. Some commands may not work. Do you still want to open it?");
                if (!result) {
                    return;
                }
                */
                // parse for testCase or testSuite
                if (checkIsTestSuite(test_suite)) {
                    // alert("Sorry, we do not support test suite of the format of an early version of Selenium IDE now.");
                    olderTestSuiteResult = test_suite.substring(0, test_suite.indexOf("<table")) + test_suite.substring(test_suite.indexOf("</body>"));
                    olderTestSuiteFile = f;
                    loadCaseIntoSuite(test_suite);
                    return;
                } else {
                    test_suite = transformVersion(test_suite);
                }
            }
            // some early version of SideeX2 without <meta>
            test_suite = addMeta(test_suite);
        }

        // append on test grid
        appendTestSuite(f, test_suite);
        return;
        // set up some veraible for recording after loading
    };
    reader.onerror = function(e) {
        console.log("Error", e);
    };
}



function deleteExistingLibrary() {
    var libraryId;
    for (var key in sideex_testSuite) {
        if (Object.prototype.hasOwnProperty.call(sideex_testSuite, key)) {
            if (key != 'count') {
                var val = sideex_testSuite[key];
                if (val && val.title == 'Record Library') {
                    libraryId = key;
                    console.log("Library deleted: ", key, val);
                    break;
                }
            }
        }
    }
    if (libraryId) {
        setSelectedSuite(libraryId);
        remove_testSuite();
    }
    var date = new Date();
    console.log(date);
}


function toggleLoader() {
    console.log('loader toggle');
    $("#loader").toggle();
    $("#blank-background").toggle();
}
