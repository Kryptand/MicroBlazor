using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.ClientsideStorage.Logic
{
    public class LocalStorageService : IStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task AddAsync<T>(string key, T data)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            await _jsRuntime.InvokeAsync<object>("window.localStorage.setItem", new[] { key, JsonConvert.SerializeObject(data) });
        }

        public async Task<T> GetSingleAsync<TInput, T>(string storeName, TInput key)
        {
            var serialisedData = await _jsRuntime.InvokeAsync<string>("window.localStorage.getItem", key);

            if (serialisedData == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(serialisedData);
        }

        public async Task DeleteAsync<TInput>(string storeName, TInput key)
        {
            await _jsRuntime.InvokeAsync<string>("blazored.localStorage.removeItem", key);
        }

        public async Task ClearAsync(string storeName) => await _jsRuntime.InvokeVoidAsync("window.localStorage.setItem", null);


        public async Task UpdateAsync<T>(string storeName, T updateItem)
        {
            await AddAsync(storeName, updateItem);

        }
        public Task AddSchemaAsync(string storeName, IEnumerable<string> indexSpecs = null, string primaryKey = "id", bool autoIncrement = true)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ListAsync<T>(string storeName)
        {
            throw new NotImplementedException();
        }


    }
}
