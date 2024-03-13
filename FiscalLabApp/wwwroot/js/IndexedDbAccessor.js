if ('undefined' === typeof window) {
    importScripts("shared.js")
}

export function initialize() {
    openDatabase()
        .then(_ => {
            console.log("database created successfully.");
        })
        .catch(error => console.error(error));
}

export function set(collectionName, value) {
    let applicationDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    applicationDb.onsuccess = function () {
        let transaction = applicationDb.result.transaction(collectionName, "readwrite");
        let collection = transaction.objectStore(collectionName)
        collection.put(value);
        
        let lastUpdateData = {
            id: LAST_UPDATE_KEY,
            value: new Date()
        }
        let lastUpdateTransaction = applicationDb.result.transaction(LAST_UPDATE_INFO_COLLECTION, "readwrite");
        let lastUpdateCollection = lastUpdateTransaction.objectStore(LAST_UPDATE_INFO_COLLECTION)
        lastUpdateCollection.put(lastUpdateData);
    }
}

export function setAll(collectionName, data) {
    let applicationDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

    applicationDb.onsuccess = function () {
        let transaction = applicationDb.result.transaction(collectionName, "readwrite");
        let collection = transaction.objectStore(collectionName)

        let clearRequest = collection.clear();

        clearRequest.onsuccess = function () {
            data.forEach(item => {
                collection.add(item);
            });

            let lastSyncData = {
                id: LAST_SYNC_KEY,
                value: new Date()
            }
            let lastSyncTransaction = applicationDb.result.transaction(LAST_SYNC_INFO_COLLECTION, "readwrite");
            let lastSyncCollection = lastSyncTransaction.objectStore(LAST_SYNC_INFO_COLLECTION)
            lastSyncCollection.put(lastSyncData);
        };
    }
}

export async function get(collectionName) {
    let request = new Promise((resolve) => {
        let applicationDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        applicationDb.onsuccess = function () {
            let transaction = applicationDb.result.transaction(collectionName, "readonly");
            let collection = transaction.objectStore(collectionName);
            let result = collection.getAll();

            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });

    return await request;
}

export async function getById(collectionName, id) {
    let request = new Promise((resolve) => {
        let blazorSchoolIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        blazorSchoolIndexedDb.onsuccess = function () {
            let transaction = blazorSchoolIndexedDb.result.transaction(collectionName, "readonly");
            let collection = transaction.objectStore(collectionName);
            let result = collection.get(id);

            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });

    return await request;
}

export async function remove(collectionName, id) {
    let request = new Promise((resolve) => {
        let blazorSchoolIndexedDb = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);
        blazorSchoolIndexedDb.onsuccess = function () {
            let transaction = blazorSchoolIndexedDb.result.transaction(collectionName, "readwrite");
            let collection = transaction.objectStore(collectionName);
            let result = collection.delete(id);

            result.onsuccess = function (e) {
                resolve(result.result);
            }
        }
    });

    return await request;
}