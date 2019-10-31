import * as LocalForage from 'localforage';

window.registerStorage = (storageConfig) => {
    if (window.Storage) {
        return;
    }
    const config = !!storageConfig ? storageConfig : getDefaultConfig();
    window.Storage = new Storage(config);
};
window.destroyStorage = () => {
    if (!window.Storage) {
        return;
    }
    window.Storage = null;
}
window.storageReady = () => {
    if (!window.Storage) {
        throw new Error('Storage is not defined, create a new storage instance by calling registerStorage');
    }
    return window.Storage.ready();
};
window.storageKeys = () => {
    if (!window.Storage) {
        throw new Error('Storage is not defined, create a new storage instance by calling registerStorage');
    }
    return window.Storage.keys();
};
window.getStorageValueByKey = (key) => {
    if (!window.Storage) {
        throw new Error('Storage is not defined, create a new storage instance by calling registerStorage');
    }
    return window.Storage.get(key);
};
window.setStorageValueByKey = (key, value) => {
    if (!window.Storage) {
        throw new Error('Storage is not defined, create a new storage instance by calling registerStorage');
    }
    return window.Storage.set(key, value);
};
window.removeStorageValueByKey = (key) => {
    if (!window.Storage) {
        throw new Error('Storage is not defined, create a new storage instance by calling registerStorage');
    }
    return window.Storage.set(key, value);
};
window.clearStorage = () => {
    if (!window.Storage) {
        throw new Error('Storage is not defined, create a new storage instance by calling registerStorage');
    }
    return window.Storage.clear();
};
window.storageLength = () => {
    if (!window.Storage) {
        throw new Error('Storage is not defined, create a new storage instance by calling registerStorage');
    }
    return window.Storage.length();
};
window.storageKeys = () => {
    if (!window.Storage) {
        throw new Error('Storage is not defined, create a new storage instance by calling registerStorage');
    }
    return window.Storage.keys();
};

export class Storage {
    _dbPromise;
    _driver;

    constructor(config) {
        this._dbPromise = new Promise((resolve, reject) => {
            if (typeof process !== 'undefined') {
                const noopDriver = getNoopDriver();
                resolve(noopDriver);
                return;
            }

            let db;

            const defaultConfig = getDefaultConfig();
            const actualConfig = Object.assign(defaultConfig, config || {});

            LocalForage.defineDriver(CordovaSQLiteDriver)
                .then(() => {
                    db = LocalForage.createInstance(actualConfig);
                })
                .then(() =>
                    db.setDriver(this._getDriverOrder(actualConfig.driverOrder))
                )
                .then(() => {
                    this._driver = db.driver();
                    resolve(db);
                })
                .catch(reason => reject(reason));
        });
    }

    /**
     * Get the name of the driver being used.
     * @returns Name of the driver
     */
    get driver(){
        return this._driver;
    }

    /**
     * Reflect the readiness of the store.
     * @returns Returns a promise that resolves when the store is ready
     */
    ready() {
        return this._dbPromise;
    }

    /** @hidden */
    _getDriverOrder(driverOrder) {
        return driverOrder.map(driver => {
            switch (driver) {
                case 'indexeddb':
                    return LocalForage.INDEXEDDB;
                case 'websql':
                    return LocalForage.WEBSQL;
                case 'localstorage':
                    return LocalForage.LOCALSTORAGE;
            }
        });
    }

    /**
     * Get the value associated with the given key.
     * @param key the key to identify this value
     * @returns Returns a promise with the value of the given key
     */
    get(key) {
        return this._dbPromise.then(db => db.getItem(key));
    }

    /**
     * Set the value for the given key.
     * @param key the key to identify this value
     * @param value the value for this key
     * @returns Returns a promise that resolves when the key and value are set
     */
    set(key,value){
        return this._dbPromise.then(db => db.setItem(key, value));
    }

    /**
     * Remove any value associated with this key.
     * @param key the key to identify this value
     * @returns Returns a promise that resolves when the value is removed
     */
    remove(key) {
        return this._dbPromise.then(db => db.removeItem(key));
    }

    /**
     * Clear the entire key value store. WARNING: HOT!
     * @returns Returns a promise that resolves when the store is cleared
     */
    clear() {
        return this._dbPromise.then(db => db.clear());
    }

    /**
     * @returns Returns a promise that resolves with the number of keys stored.
     */
    length(){
        return this._dbPromise.then(db => db.length());
    }

    /**
     * @returns Returns a promise that resolves with the keys in the store.
     */
    keys() {
        return this._dbPromise.then(db => db.keys());
    }

    /**
     * Iterate through each key,value pair.
     * @param iteratorCallback a callback of the form (value, key, iterationNumber)
     * @returns Returns a promise that resolves when the iteration has finished.
     */
    forEach(
        iteratorCallback
    ) {
        return this._dbPromise.then(db => db.iterate(iteratorCallback));
    }
}

/** @hidden */
export function getDefaultConfig() {
    return {
        name: '_kryptandStorage',
        storeName: '_kryptandKV',
        dbKey: '_kryptandKey',
        driverOrder: ['indexeddb', 'websql', 'localstorage']
    };
};

function getNoopDriver() {
    const noop = () => { };
    const driver = {
        getItem: noop,
        setItem: noop,
        removeItem: noop,
        clear: noop,
        length: () => 0,
        keys: () => [],
        iterate: noop
    };
    return driver;
};