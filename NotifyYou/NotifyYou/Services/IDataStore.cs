using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NotifyYou.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddUpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
