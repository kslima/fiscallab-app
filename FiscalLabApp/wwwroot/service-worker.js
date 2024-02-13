// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
if ('undefined' === typeof window) {
    importScripts("./js/shared.js")
}
self.addEventListener('fetch', () => {
});

self.addEventListener('sync', event => {
    if (event.tag === SYNC_PROCESS_NAME) {
        openDatabase()
            .then(db => {
                fetchDataAndPopulateDB(db);
                if ('serviceWorker' in navigator && 'ready' in navigator.serviceWorker) {
                    navigator.serviceWorker.ready.then(registration => {
                        registration.active.postMessage({type: 'notification', message: 'Sync completed'});
                    });
                }
            })
            .catch(error => console.error(error));
    }
});

function fetchDataAndPopulateDB(db) {

    let collectionsToSync = [PLANTS_COLLECTION, ASSOCIATIONS_COLLECTION, MENUS_COLLECTION, VISITS_COLLECTION];
    const promises = collectionsToSync.map(collection => listAllAsync(db, collection));

    Promise.all(promises)
        .then(results => {
            const dataToSend = {
                plants: results[0],
                associations: results[1],
                menus: results[2],
                visits: results[3].filter(v => v.finishedAt !== null && v.finishedAt !== undefined),
            };

            return fetch('http://localhost:7001/synchronization', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(dataToSend),
            });
        })
        .then(response => response.json())
        .then(apiResponse => {

            let plants = apiResponse.data[PLANTS_COLLECTION];
            let associations = apiResponse.data[ASSOCIATIONS_COLLECTION];
            let menus = apiResponse.data[MENUS_COLLECTION];
            let visits = apiResponse.data[VISITS_COLLECTION];

            const promises = [
                updateDatabaseWithApiData(db, PLANTS_COLLECTION, plants),
                updateDatabaseWithApiData(db, ASSOCIATIONS_COLLECTION, associations),
                updateDatabaseWithApiData(db, MENUS_COLLECTION, menus),
                updateDatabaseWithApiData(db, SYNCED_VISITS_COLLECTION, visits),
                deleteItems(db, VISITS_COLLECTION, visits.map(v => v.id))
            ];

            return Promise.all(promises);
        })
        .then(messages => {
            messages.forEach(message => console.log(message));
        })
        .catch(error => {
            console.error('Erro ao listar, enviar dados para a API e atualizar o banco:', error);
        });
}

function listAllAsync(db, storeName) {
    return new Promise((resolve, reject) => {
        let transaction = db.transaction([storeName], "readonly");
        let store = transaction.objectStore(storeName);

        let request = store.getAll();

        request.onsuccess = function (event) {
            resolve(event.target.result);
        };

        request.onerror = function (event) {
            reject("Error listing items from " + storeName + ": " + event.target.error);
        };
    });
}

function updateDatabaseWithApiData(db, storeName, data) {
    console.log(storeName)
    return new Promise((resolve, reject) => {
        let transaction = db.transaction([storeName], "readwrite");
        let store = transaction.objectStore(storeName);

        let clearRequest = store.clear();

        clearRequest.onsuccess = function () {
            data.forEach(item => {
                store.add(item);
            });

            resolve("Database updated with API data for " + storeName);
        };

        clearRequest.onerror = function (event) {
            reject("Error clearing store " + storeName + ": " + event.target.error);
        };
    });
}

function deleteItems(db, storeName, ids) {
    console.log(storeName)
    return new Promise((resolve, reject) => {
        let transaction = db.transaction([storeName], "readwrite");
        let store = transaction.objectStore(storeName);

        const deletePromises = ids.map(id => new Promise((resolve, reject) => {
            let deleteRequest = store.delete(id);
            deleteRequest.onsuccess = () => resolve();
            deleteRequest.onerror = (event) => reject("Error deleting item from store " + storeName + ": " + event.target.error);
        }));

        Promise.all(deletePromises)
            .then(() => resolve("Items deleted from " + storeName))
            .catch(error => reject(error));
    });
}