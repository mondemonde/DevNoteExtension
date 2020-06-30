



chrome.runtime.getBackgroundPage(function(bg) {


    $('#someUploadIdentifier').bind('change', function (e) {
        var f;
        f = e.target.files || [{name: this.value}];
        readLatestSuite(f[0]);
    });
    



    // Relevant function at the background page. In your specific example:
    //bg.clearCache();
    //D:\_ChromeExtension\katalon-recorder-samples-master\katalon-recorder-samples-master
      var preCtrl =  $('#katalonScript');
    preCtrl.load("latest.html");


    //readLatestSuite(preCtrl.val());

// This data/text below is local to the JS script, so we are allowed to send it!
//uploadFile({'hello!':'how are you?'});

//uploadFile('hello! MOND');
uploadFile(preCtrl.val());


    console.log('devnote running on bg.');
   // alert('oi');

});






function uploadFile (data) {
    // define data and connections
//var blob = new Blob([JSON.stringify(data)]);
//var url = URL.createObjectURL(blob);


var mystring = data;////"Hello World!";
var blob = new Blob([mystring], {
    type: 'text/plain'
});

var url = URL.createObjectURL(blob);

readLatestSuite(blob);

// var xhr = new XMLHttpRequest();
// xhr.open('POST', 'devnote.html', true);

//     // define new form
// var formData = new FormData();
// formData.append('someUploadIdentifier', blob, 'someFileName.txt');

//     // action after uploading happens
// xhr.onload = function(e) {
//     console.log("File uploading completed!");
// };

//     // do the uploading
// console.log("File uploading started!");
// xhr.send(formData);




}

// This data/text below is local to the JS script, so we are allowed to send it!
//uploadFile({'hello!':'how are you?'});

function readLatestSuite(f) {
    var reader = new FileReader();
   // if (!f.name.includes("htm")) return;
    reader.readAsText(f);

    reader.onload = function(event) {
        //setTimeout(saveData, 0);
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
