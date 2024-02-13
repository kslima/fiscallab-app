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