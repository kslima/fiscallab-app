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
                dotNetObjRef.invokeMethodAsync("SetOnlineStatusColor", navigator.onLine, isWiFi);
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

window.notifications = {
    notify: function(message) {
        DotNet.invokeMethodAsync("FiscalLabApp", "SyncCompleted", message);
    }
};

