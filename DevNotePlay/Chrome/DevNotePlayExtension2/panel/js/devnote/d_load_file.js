function d_readSuite(f, isFile = true) {
    if (isFile) {
        var reader = new FileReader();
        if (!f.name.includes("htm")) return;
        reader.readAsText(f);
    
        reader.onload = function(event) {
            setTimeout(saveData, 0);
            var test_suite = reader.result;
            d_suiteReader(event, test_suite);
            // append on test grid
            d_appendTestSuite(f, test_suite);
        };
        reader.onerror = function(e) {
            console.log("Error", e);
        };
    }
    else {
        d_suiteReader(null, f);
        d_appendTestSuite(null, f, 'Record Library');
    }
}

function d_suiteReader(event, test_suite) {
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

    return;
    // set up some veraible for recording after loading
}