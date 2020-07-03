function d_appendTestSuite(suiteFile, suiteResult, fileName) {
    // append on test grid
    var id = "suite" + sideex_testSuite.count;
    sideex_testSuite.count++;
    var suiteFileName;
    var suiteFileNameExt;

    if (fileName) {
        suiteFileName = suiteFileNameExt = fileName;
    }
    else {
        suiteFileNameExt = suiteFile.name;
        if (suiteFile.name.lastIndexOf(".") >= 0) {
            suiteFileName = suiteFile.name.substring(0, suiteFile.name.lastIndexOf("."));
        } else {
            suiteFileName = suiteFile.name;
        }
    }

    addTestSuite(suiteFileName, id);
    // name is used for download
    sideex_testSuite[id] = {
        file_name: suiteFileNameExt,
        title: suiteFileName
    };

    test_case = suiteResult.match(/<table[\s\S]*?<\/table>/gi);
    if (test_case) {
        for (var i = 0; i < test_case.length; ++i) {
            readCase(test_case[i]);
        }
    }

    setSelectedSuite(id);
    clean_panel();
}