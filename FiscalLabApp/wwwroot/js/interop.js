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

function downloadPdf(base64Pdf, fileName) {
    let link = document.createElement("a");
    link.href = "data:application/pdf;base64," + base64Pdf;
    link.download = fileName;
    
    document.body.appendChild(link);
    link.click();
    
    document.body.removeChild(link);
}

window.notifications = {
    notify: function(message) {
        DotNet.invokeMethodAsync("FiscalLabApp", "SyncCompleted", message);
    }
};

