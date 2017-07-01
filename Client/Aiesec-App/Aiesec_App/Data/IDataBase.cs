using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    public interface IDataBase<T>
    {
        Task<int> SaveItemAsync(T item);

        Task<int> DeleteItemAsync(T item);

        Task<List<T>> GetItemsAsync();
    }
}
