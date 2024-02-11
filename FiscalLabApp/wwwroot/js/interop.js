// wwwroot/interop.js
window.saveAsFile = function (fileName, content) {
    const blob = base64toBlob(content);
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};

function base64toBlob(base64) {
    const binaryString = window.atob(base64);
    const len = binaryString.length;
    const bytes = new Uint8Array(len);
    for (let i = 0; i < len; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }
    return new Blob([bytes]);
}

var jsFunctions = {};

jsFunctions.registerOnlineStatusHandler = function (dotNetObjRef) {
    function onlineStatusHandler() {
        dotNetObjRef.invokeMethodAsync("SetOnlineStatusColor", navigator.onLine);
        registerSyncTask();
    };
    onlineStatusHandler();
    window.addEventListener("online", onlineStatusHandler);
    window.addEventListener("offline", onlineStatusHandler);
}

function registerSyncTask() {
    if (navigator.serviceWorker.controller !== null && 'SyncManager' in window) {
        navigator.serviceWorker.ready.then(registration => {
            registration.sync.register('sincronizacao-background');
        });
    }
}