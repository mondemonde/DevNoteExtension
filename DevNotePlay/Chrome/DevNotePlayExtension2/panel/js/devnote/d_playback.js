window.onload = function() {
    var saveToLibraryButton = document.getElementById("save-to-library");
    var refreshLibraryButton = document.getElementById("refresh-library");

    saveToLibraryButton.addEventListener("click", function() {
        openCreateEventWindow();
    });
    refreshLibraryButton.addEventListener("click", function() {
        loadRecordingLibrary();
    });

    setTimeout(() => {
        loadRecordingLibrary();
    }, 1000);
}

function loadRecordingLibrary() {
    getRecordLibrary();
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
    // var date = new Date();
    // console.log(date);
}

function toggleLoader() {
    // console.log('loader toggle');
    $("#loader").toggle();
    $("#blank-background").toggle();
}

function openCreateEventWindow() {
    $("#d_export").click();
}