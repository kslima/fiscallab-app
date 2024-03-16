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

async function createPdfObjectUrl(pdfBytes, elementId) {
    const blob = new Blob([new Uint8Array(pdfBytes)], { type: 'application/pdf' });
    const objectUrl = URL.createObjectURL(blob);
    const iframe = document.getElementById(elementId);
    iframe.src = objectUrl;
    // We don't need to keep the object url, let's release the memory
    URL.revokeObjectURL(objectUrl);
}

window.loadPdfFromBase64 = function (base64Pdf, targetElementId) {
    // Converte a string base64 para um ArrayBuffer
    var binaryPdf = atob(base64Pdf);
    var length = binaryPdf.length;
    var bytes = new Uint8Array(length);

    for (var i = 0; i < length; i++) {
        bytes[i] = binaryPdf.charCodeAt(i);
    }

    // Carrega o PDF a partir do ArrayBuffer
    pdfjsLib.getDocument({ data: bytes }).promise.then(function (pdf) {
        for (let pageNumber = 1; pageNumber <= pdf.numPages; pageNumber++) {
            pdf.getPage(pageNumber).then(function (page) {
                let scale = 1.5;
                let viewport = page.getViewport({ scale: scale });
                let canvas = document.createElement("canvas");
                let context = canvas.getContext("2d");
                canvas.height = viewport.height;
                canvas.width = viewport.width;
                let renderContext = {
                    canvasContext: context,
                    viewport: viewport
                };
                page.render(renderContext);
                document.getElementById(targetElementId).appendChild(canvas);
            });
        }
    });
}

window.savePdfFromBase64 = function (base64Pdf) {
    // Converter a string base64 para um ArrayBuffer
    var binaryPdf = atob(base64Pdf);
    var length = binaryPdf.length;
    var bytes = new Uint8Array(length);

    for (var i = 0; i < length; i++) {
        bytes[i] = binaryPdf.charCodeAt(i);
    }

    // Criar um Blob com os dados do PDF
    var blob = new Blob([bytes], { type: "application/pdf" });

    // Criar um link para download do Blob
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = "download.pdf";
    link.click();
}