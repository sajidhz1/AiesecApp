using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    public interface IRestService<T>
    {
        Task<List<T>> RefreshDataAsync(string url, bool authorized = true);

        Task<List<T>> GetLatestAsync(string url);

        Task<bool> SaveItemAsync(string url,T item, bool isNewItem);

        Task<bool> SaveItemsAsync(List<T> items);

        Task<bool> UpdateItemAsync(string url, string id, T item);

        Task<bool> DeleteItemAsync(string url, string id);
    }
}
