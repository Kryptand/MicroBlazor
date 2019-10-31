using System.Threading.Tasks;

namespace Shared.ClientSideStorage.Logic.Contracts
{
    interface IStorageService
    {
        ValueTask ClearAsync();
        ValueTask DestroyStorage();
        Task<T> GetItemAsync<T>(string key);
        Task<T> KeysAsync<T>();
        Task<int> LengthAsync();
        ValueTask RegisterStorage();
        Task RemoveAsync(string key);
        Task SetItemAsync(string key, object data);
        ValueTask StorageReady();
    }
}