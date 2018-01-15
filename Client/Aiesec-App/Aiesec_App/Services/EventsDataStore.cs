using Aiesec_App.Data;
using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Aiesec_App.Services.EventsDataStore))]
namespace Aiesec_App.Services
{
    public class EventsDataStore : IDataStore<EventItem>
    {
        bool isInitialized;
        List<EventItem> items;

        public async Task<bool> AddItemAsync(EventItem item)
        {
            await SyncAsync();
            
            bool httpStatus = await App.EventsManager.SaveTaskAsync(Constants.URL_EVENTS, item, true);
            if (httpStatus)
            {
                items.Add(item);
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(EventItem item)
        {
            await SyncAsync();

            var _item = items.Where((EventItem arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<EventItem> GetItemAsync(string id)
        {
            await SyncAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<EventItem>> GetItemsAsync(bool forceRefresh = false)
        {
            await SyncAsync();

            return await Task.FromResult(items);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }

        public async Task<IEnumerable<EventItem>> SyncAsync()
        {
            items = new List<EventItem>();
            //var _localItems = await App.EventsDatabase.Get();

            //if (_localItems.Count <= 0)
            //{
            //    await InitializeAsync();
            //    _localItems = await App.EventsDatabase.Get();
            //}            

            //foreach (EventItem item in _localItems)
            //{
            //    item.EventImage = "http://lorempixel.com/400/200/";
            //    items.Add(item);
            //}
            await InitializeAsync();
            return await Task.FromResult(items);
        }
    

        public async Task<bool> UpdateItemAsync(EventItem item)
        {
            await SyncAsync();

            var _item = items.Where((EventItem arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            //if (isInitialized)
            //    return;

            items = new List<EventItem>();

            var _serverItems = await App.EventsManager.GetItemsAsync(Constants.URL_EVENTS);        
    

            foreach (EventItem item in _serverItems)
            {              
                items.Add(item);
            }

            isInitialized = true;
        }
    }
}
