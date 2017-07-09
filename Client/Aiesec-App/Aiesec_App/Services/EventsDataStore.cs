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

            items.Add(item);
            await App.EventsDatabase.Insert(item);
            await App.EventsManager.SaveTaskAsync(item, true);

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

        public async Task SyncAsync()
        {
            items = new List<EventItem>();
            var _localItems = await App.EventsDatabase.Get();

            if (_localItems.Count <= 0)
            {
                await InitializeAsync();
                _localItems = await App.EventsDatabase.Get();
            }            

            foreach (EventItem item in _localItems)
            {
                items.Add(item);
            }
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
            if (isInitialized)
                return;

            items = new List<EventItem>();

            var _serverItems = await App.EventsManager.GetItemsAsync(Constants.ToDoList);
            var _localItems = await App.EventsDatabase.Get();

            var _insertItems = _serverItems.Except(_localItems, new IdComparer()).ToList();
            foreach (EventItem item in _insertItems)
            {
                await App.EventsDatabase.Insert(item);
            }

            var _updateItems = _serverItems.Except(_insertItems, new IdComparer()).ToList();
            foreach (EventItem item in _updateItems)
            {
                await App.EventsDatabase.Update(item);
            }

            _localItems = await App.EventsDatabase.Get();

            foreach (EventItem item in _localItems)
            {
                //item.EventImage = new UriImageSource
                //{
                //    Uri = new Uri("https://upload.wikimedia.org/wikipedia/en/5/5f/Original_Doge_meme.jpg")
                //};
                items.Add(item);
            }

            items.Add(new EventItem() {

                Name = "lalalal"
                ,Notes = "Testing"
            });

            items.Add(new EventItem()
            {

                Name = "lalalal"
                ,
                Notes = "Testing2"
            });

            isInitialized = true;
        }
    }
}
