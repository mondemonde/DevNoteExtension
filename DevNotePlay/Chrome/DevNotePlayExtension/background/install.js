// KAT-BEGIN show docs on install or upgrade from 1.0
chrome.runtime.onInstalled.addListener(function (details) {

    // if (details.reason === 'install') {
    //     chrome.tabs.create({
    //         url: 'https://alpha.quickreach.co'
    //     });
    // }
    // else if (details.reason === 'update') {
    //     var previousVersion = details.previousVersion;
    //     var previousMajorVersion = previousVersion.substring(0, previousVersion.indexOf('.'));
    //     if (previousMajorVersion === '1') {
    //         chrome.tabs.create({ 'url': 'https://www.katalon.com' });
    //     }
    // }
});

chrome.downloads.onDeterminingFilename.addListener(function (item, suggest) {
    suggest({filename: '..', conflictAction: 'overwrite'});
});

// chrome.runtime.setUninstallURL('https://store.katalon.com/?utm_source=chrome%20store&utm_campaign=uninstalled%20plugin');
// KAT-END