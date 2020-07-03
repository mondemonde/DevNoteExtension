function saveToLibrary(xml, html) {
    // try {
    //     toggleLoader();
    if (!xml || !html) {
        return;
    }
    var url = config.devNoteExtenApiUrl + "window/create-event";
    var settings = {
        "url": url,
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "xml": xml,
            "html": html
        })
    };
    $.ajax(settings).done(function (response) {
        console.log(response);
        // toggleLoader();
    });
    // }
    // catch(err) {
    //     toggleLoader();
    //     console.log(err.message)
    // }
}