using Microsoft.JSInterop;
using Shared.ClientSideStorage.Logic.Contracts;
using System;
using System.Threading.Tasks;

namespace Shared.ClientSideStorage.Logic
{
    internal class StorageService : IAsyncDisposable, IStorageService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly StorageConfig _storageConfig;

        public StorageService(IJSRuntime jsRuntime, StorageConfig storageConfig)
        {
            _jsRuntime = jsRuntime;
            _storageConfig = storageConfig;
        }

        public async ValueTask RegisterStorage()
        {
            await _jsRuntime.InvokeVoidAsync("registerStorage", _storageConfig);
        }
        public async ValueTask DestroyStorage()
        {
            await _jsRuntime.InvokeVoidAsync("destroyStorage");
        }
        public async ValueTask StorageReady()
        {
            await _jsRuntime.InvokeVoidAsync("storageReady");
        }
        public async ValueTask ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("clearStorage");
        }

        public async Task<T> GetItemAsync<T>(string key)
        {
            return await _jsRuntime.InvokeAsync<T>("getStorageValueByKey", key);
        }
        public async Task<T> KeysAsync<T>()
        {
            return await _jsRuntime.InvokeAsync<T>("storageKeys");
        }
        public async Task<int> LengthAsync()
        {
            return await _jsRuntime.InvokeAsync<int>("storageLength");
        }
        public async Task RemoveAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("removeStorageValueByKey", key);
        }
        public async Task SetItemAsync(string key, object data)
        {
            await _jsRuntime.InvokeVoidAsync("setStorageValueByKey", new[] { key, data });
        }

        ValueTask IAsyncDisposable.DisposeAsync()
        {
            return DestroyStorage();
        }
    }
}
