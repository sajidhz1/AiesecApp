using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    class EventsManager
    {
        IRestService<EventItem> restService;

        public EventsManager(IRestService<EventItem> service)
        {
            restService = service;
        }

        public Task<List<EventItem>> GetItemsAsync()
        {
            return restService.RefreshDataAsync();
        }

        public Task SaveTaskAsync(EventItem item, bool isNewItem = false)
        {
            return restService.SaveItemAsync(item, isNewItem);
        }

        public Task DeleteTaskAsync(EventItem item)
        {
            return restService.DeleteItemAsync(item.ID);
        }
    }
}
