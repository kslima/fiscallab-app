// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).

self.addEventListener('fetch', () => {
});

let CURRENT_VERSION = 1;
let DATABASE_NAME = "FiscalLabApp";

self.addEventListener('sync', event => {
    if (event.tag === 'sincronizacao-background') {
        Promise.all([
            get('plants'),
            get('associations'),
            get('menus'),
            get('visits')
        ])
            .then(results => {
                sendToBack(results)
                    .then(result => {
                        putAll(result.plants)
                            .then(message => console.log(message))
                            .catch(error => console.error(error));

                        putAll(result.associations)
                            .then(message => console.log(message))
                            .catch(error => console.error(error));

                        putAll(result.menus)
                            .then(message => console.log(message))
                            .catch(error => console.error(error));

                        putAll(result.visits)
                            .then(message => console.log(message))
                            .catch(error => console.error(error));
                    })
            })
            .catch(error => {
                console.error('Erro ao obter dados do IndexedDB:', error);
            });
    }
});

function putAll(collectionName, data) {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(DATABASE_NAME, CURRENT_VERSION);

        request.onsuccess = event => {
            const db = event.target.result;

            const transaction = db.transaction(collectionName, 'readwrite');
            const store = transaction.objectStore(collectionName);
            
            data.forEach(record => {
                const putRequest = store.put(record);
                putRequest.onerror = error => reject(error);
            });

            transaction.oncomplete = () => {
                resolve();
            };
        };

        request.onerror = event => reject(event.target.error);
    });
}

async function get(collectionName) {
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

async function sendToBack(results) {
    const body = {
        plants: results[0],
        associations: results[1],
        menus: results[2],
        visits: results[3],
    };

    fetch('http://localhost:7001/synchronization', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
    })
        .then(response => {
            if (!response.ok) {
                console.error('Resposta da API: ', response);
                throw new Error('Erro na requisição para a API');
            }
            return response.json();
        })
        .then(apiResponse => {
            console.log('Resposta da API: ', apiResponse);
        })
        .catch(error => {
            console.error('Erro ao enviar dados para a API:', error);
        });
}