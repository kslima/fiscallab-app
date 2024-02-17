let CURRENT_VERSION = 1;
let DATABASE_NAME = "FiscalLabApp";

let PLANTS_COLLECTION = "plants";
let ASSOCIATIONS_COLLECTION = "associations";
let MENUS_COLLECTION = "menus";
let VISITS_COLLECTION = "visits";
let SYNC_PROCESS_NAME = "background-sync";

function openDatabase() {
    return new Promise((resolve, reject) => {
        let request = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

        request.onerror = function (event) {
            console.error(event);
            reject("Error opening database");
        };

        request.onupgradeneeded = function (event) {
            let db = event.target.result;

            let collectionsToCreate = [PLANTS_COLLECTION, ASSOCIATIONS_COLLECTION, MENUS_COLLECTION, VISITS_COLLECTION];
            collectionsToCreate.forEach(c => {
                if (!db.objectStoreNames.contains(c)) {
                    db.createObjectStore(c, {keyPath: "id"});
                }
            });
        };

        request.onsuccess = function () {
            resolve(request.result);
        };
    });
}