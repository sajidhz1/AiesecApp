using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Aiesec_App.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(Aiesec_App.Services.MockDataStore))]
namespace Aiesec_App.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        bool isInitialized;
        List<Item> items;

        public async Task<bool> AddItemAsync(Item item)
        {
            await InitializeAsync();

            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            await InitializeAsync();

            var _item = items.Where((Item arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            await InitializeAsync();

            var _item = items.Where((Item arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<Item>();

            var _serverItems  = await App.Manager.GetItemsAsync();
            var _localItems = await App.Database.GetItemsAsync();

            var _newItems  = _serverItems.Except(_localItems, new IdComparer()).ToList();

            foreach (Item item in _newItems)
            {
                await App.Database.SaveItemAsync(item);
            }

            _localItems = await App.Database.GetItemsAsync();

            foreach (Item item in _localItems)
            {
                items.Add(item);
            }

            isInitialized = true;
        }
    }
}
