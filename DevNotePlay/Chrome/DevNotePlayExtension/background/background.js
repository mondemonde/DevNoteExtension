
var master = {};
var clickEnabled = true;

// open main window
function openPanel(tab) {

    let contentWindowId = tab.windowId;
    if (master[contentWindowId] != undefined) {
        browser.windows.update(master[contentWindowId], {
            focused: true
        }).catch(function(e) {
            master[contentWindowId] == undefined;
            openPanel(tab);
        });
        return;
    } else if (!clickEnabled) {
        return;
    }

    clickEnabled = false;
    setTimeout(function() {
        clickEnabled = true;
    }, 1000);

    // open GUI with specified size
    var f = function(height, width) {
        browser.windows.create({
            url: browser.runtime.getURL("panel/index.html"),
            type: "popup",
            height: height,
            width: width
        }).then(function waitForPanelLoaded(panelWindowInfo) {
            return new Promise(function(resolve, reject) {
                let count = 0;
                let interval = setInterval(function() {
                    if (count > 100) {
                        reject("SideeX editor has no response");
                        clearInterval(interval);
                    }

                    browser.tabs.query({
                        active: true,
                        windowId: panelWindowInfo.id,
                        status: "complete"
                    }).then(function(tabs) {
                        if (tabs.length != 1) {
                            count++;
                            return;
                        } else {
                            master[contentWindowId] = panelWindowInfo.id;
                            if (Object.keys(master).length === 1) {
                                createMenus();
                            }
                            resolve(panelWindowInfo);
                            clearInterval(interval);
                        }
                    })
                }, 200);
            });
        }).then(function bridge(panelWindowInfo){
            return browser.tabs.sendMessage(panelWindowInfo.tabs[0].id, {
                selfWindowId: panelWindowInfo.id,
                commWindowId: contentWindowId
            });
        }).catch(function(e) {
            console.log(e);
        });
    };

    // get previous window size, and open the window
    getWindowSize(f);
}

browser.browserAction.onClicked.addListener(openPanel);

browser.windows.onRemoved.addListener(function(windowId) {
    let keys = Object.keys(master);
    for (let key of keys) {
        if (master[key] === windowId) {
            delete master[key];
            if (keys.length === 1) {
                browser.contextMenus.removeAll();
            }
        }
    }
});

// context menu
function createMenus() {
    browser.contextMenus.create({
        id: "verifyText",
        title: "verifyText",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
    browser.contextMenus.create({
        id: "verifyTitle",
        title: "verifyTitle",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
    browser.contextMenus.create({
        id: "verifyValue",
        title: "verifyValue",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
    browser.contextMenus.create({
        id: "assertText",
        title: "assertText",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
    browser.contextMenus.create({
        id: "assertTitle",
        title: "assertTitle",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
    browser.contextMenus.create({
        id: "assertValue",
        title: "assertValue",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
    browser.contextMenus.create({
        id: "storeText",
        title: "storeText",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
    browser.contextMenus.create({
        id: "storeTitle",
        title: "storeTitle",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
    browser.contextMenus.create({
        id: "storeValue",
        title: "storeValue",
        documentUrlPatterns: ["<all_urls>"],
        contexts: ["all"]
    });
}

var port;
browser.contextMenus.onClicked.addListener(function(info, tab) {
    port.postMessage({ cmd: info.menuItemId });
});

browser.runtime.onConnect.addListener(function(m) {
    port = m;
});

chrome.windows.onRemoved.addListener(function(windowid) {
    chrome.windows.getAll(function(windows) {
        let normalWindows = windows.filter(function(window) {
            return window.type == "normal";
        });
        if (normalWindows.length == 0) {
            for (let i = 0; i < windows.length; i++) {
                chrome.windows.remove(windows[i].id);
            }
        }
    });
});