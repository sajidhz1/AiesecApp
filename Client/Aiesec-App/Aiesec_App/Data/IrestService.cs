using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    public interface IRestService<T>
    {
        Task<List<T>> RefreshDataAsync(string url);

        Task<List<T>> GetLatestAsync(string url);

        Task<bool> SaveItemAsync(string url,T item, bool isNewItem);

        Task<bool> SaveItemsAsync(List<T> items);

        Task<bool> UpdateItemAsync(T item);

        Task<bool> DeleteItemAsync(string id);
    }
}
