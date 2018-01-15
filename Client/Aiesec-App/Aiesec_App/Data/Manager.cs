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

        public Task<List<T>> GetItemsAsync(string endPointUrl, bool authorized = true)
        {
            return restService.RefreshDataAsync(endPointUrl, authorized);
        }

        public Task<bool> SaveTaskAsync(string url, T item, bool isNewItem = false)
        {
            return restService.SaveItemAsync(url, item, isNewItem);
        }

        public Task<bool> UpdateTaskAsync(string url, string id, T item)
        {
            return restService.UpdateItemAsync(url,  id,  item);
        }

        //id = Item.ID
        public Task<bool> DeleteTaskAsync(string url, string id)
        {
            return restService.DeleteItemAsync(url, id);
        }
    }
}
