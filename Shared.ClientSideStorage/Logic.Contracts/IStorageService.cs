using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared.ClientsideStorage.Logic
{
    public interface IStorageService
    {
        Task AddAsync<T>(string storeName, T valueItem);
        Task AddSchemaAsync(string storeName, IEnumerable<string> indexSpecs = null, string primaryKey = "id",bool autoIncrement=true);
        Task ClearAsync(string storeName);
        Task DeleteAsync<TInput>(string storeName, TInput key);
        Task<T> GetSingleAsync<TInput, T>(string storeName, TInput key);
        Task<IEnumerable<T>> ListAsync<T>(string storeName);
        Task UpdateAsync<T>(string storeName, T updateItem);
    }
}