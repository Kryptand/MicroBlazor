using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TG.Blazor.IndexedDB;

namespace Shared.ClientsideStorage.Logic
{
    public class StorageService : IStorageService
    {
        private readonly IndexedDBManager _dbManager;
        public StorageService(IndexedDBManager
             dBManager)
        {
            _dbManager = dBManager;
        }
        public async Task AddAsync<T>(string storeName, T valueItem)
        {
            var newRecord = new StoreRecord<T>
            {
                Storename = storeName,
                Data = valueItem
            };
      
            await _dbManager.AddRecord(newRecord);
        }
        public async Task<T> GetSingleAsync<TInput, T>(string storeName, TInput key)
        {
            return await _dbManager.GetRecordById<TInput, T>(storeName, key);
        }
        public async Task<IEnumerable<T>> ListAsync<T>(string storeName)
        {
            return await _dbManager.GetRecords<T>(storeName);
        }
        public async Task UpdateAsync<T>(string storeName, T updateItem)
        {
            var updateRecord = new StoreRecord<T>
            {
                Storename = storeName,
                Data = updateItem
            };

            await _dbManager.UpdateRecord(updateRecord);
        }
        public async Task DeleteAsync<TInput>(string storeName, TInput key)
        {
            await _dbManager.DeleteRecord<TInput>(storeName, key);
        }
        public async Task ClearAsync(string storeName)
        {
            await _dbManager.ClearStore(storeName);
        }
        public async Task AddSchemaAsync(string storeName, IEnumerable<string> indexSpecs = null, string primaryKey = "id",bool autoIncrement=true)
        {
      
            var newStoreSchema = new StoreSchema
            {
                Name = storeName,
                PrimaryKey = new IndexSpec { Name = primaryKey, KeyPath = primaryKey, Auto = autoIncrement }
            };

            await _dbManager.AddNewStore(newStoreSchema);
        }

    }
}
