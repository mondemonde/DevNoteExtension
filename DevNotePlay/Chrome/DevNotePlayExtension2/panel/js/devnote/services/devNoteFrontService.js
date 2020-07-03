function getRecordLibrary() {
    toggleLoader();
    var url = config.devNoteFrontApiUrl + "recordings";
    var settings = {
        "url": url,
        "method": "GET",
        "timeout": 0,
    };
    $.ajax(settings).done(function (response) {
        // var date = new Date();
        // console.log(date);
        deleteExistingLibrary();
        d_readSuite(response, false);

        toggleLoader();
    });
}