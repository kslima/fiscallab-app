if ('undefined' === typeof window) {
    importScripts("shared.js")
}

var jsFunctions = {};

jsFunctions.registerOnlineStatusHandler = function (dotNetObjRef) {
    function onlineStatusHandler() {
        getConnectionType()
            .then(connectionType => {
                const isWiFi = connectionType === '4g';
                if (navigator.onLine && isWiFi) {
                    //registerSyncTask();
                }
                dotNetObjRef.invokeMethodAsync("SetOnlineStatus", navigator.onLine, isWiFi);
            });
    }

    onlineStatusHandler();
    window.addEventListener("online", onlineStatusHandler);
    window.addEventListener("offline", onlineStatusHandler);
}

function getConnectionType() {
    return new Promise((resolve) => {
        if (navigator.connection && navigator.connection.effectiveType) {
            resolve(navigator.connection.effectiveType);
        } else {
            resolve('Connection type not supported');
        }
    });
}

function registerSyncTask() {
    if (navigator.serviceWorker.controller !== null && 'SyncManager' in window) {
        navigator.serviceWorker.ready.then(registration => {
            registration.sync.register(SYNC_PROCESS_NAME);
        });
    }
}

window.savePdf = function (base64Pdf, name) {
    let binaryPdf = atob(base64Pdf);
    let length = binaryPdf.length;
    let bytes = new Uint8Array(length);

    for (let i = 0; i < length; i++) {
        bytes[i] = binaryPdf.charCodeAt(i);
    }
    
    let blob = new Blob([bytes], { type: "application/pdf" });
    let link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = name;
    link.click();
}

window.removeFocusFromAllElements = function() {
    document.activeElement.blur();
};