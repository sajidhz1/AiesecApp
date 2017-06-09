using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    public interface IRestService<T>
    {
        Task<List<T>> RefreshDataAsync();

        Task SaveItemAsync(T item, bool isNewItem);

        Task DeleteItemAsync(string id);
    }
}
