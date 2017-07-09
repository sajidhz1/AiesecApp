using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
   public class Manager<T>
    {
        IRestService<T> restService;

        public Manager(IRestService<T> service)
        {
            restService = service;
        }

        public Task<List<T>> GetItemsAsync(string endPointUrl)
        {
            return restService.RefreshDataAsync(endPointUrl);
        }

        public Task<bool> SaveTaskAsync(T item, bool isNewItem = false)
        {
            return restService.SaveItemAsync(item, isNewItem);
        }

        public Task<bool> UpdateTaskAsync(T item)
        {
            return restService.UpdateItemAsync(item);
        }

        //id = Item.ID
        public Task<bool> DeleteTaskAsync(string id)
        {
            return restService.DeleteItemAsync(id);
        }
    }
}
