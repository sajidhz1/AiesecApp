using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
   public class ItemManager
    {
        IRestService<Item> restService;

        public ItemManager(IRestService<Item> service)
        {
            restService = service;
        }

        public Task<List<Item>> GetItemsAsync()
        {
            return restService.RefreshDataAsync();
        }

        public Task SaveTaskAsync(Item item, bool isNewItem = false)
        {
            return restService.SaveItemAsync(item, isNewItem);
        }

        public Task DeleteTaskAsync(Item item)
        {
            return restService.DeleteItemAsync(item.ID);
        }
    }
}
