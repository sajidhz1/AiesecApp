using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Aiesec_App.Models;

using Xamarin.Forms;

[assembly: Dependency(typeof(Aiesec_App.Services.ComplainDataStore))]
namespace Aiesec_App.Services
{
    public class ComplainDataStore : IDataStore<ComplainItem>
    {
        bool isInitialized;
        List<ComplainItem> items;

        public async Task<bool> AddItemAsync(ComplainItem item)
        {
            await InitializeAsync();

            items.Add(item);
            await App.ItemsDatabase.Insert(item);
            await App.ItemsManager.SaveTaskAsync(item, true);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ComplainItem item)
        {
            await InitializeAsync();

            var _item = items.Where((ComplainItem arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(ComplainItem item)
        {
            await InitializeAsync();

            var _item = items.Where((ComplainItem arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<ComplainItem> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<ComplainItem>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(items);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public async Task<bool> SyncAsync()
        {
            //try
            //{
            //    //if (!CrossConnectivity.Current.IsConnected || !Settings.NeedsSync)
            //    //    return;

            //    List<ComplainItem> itemsNotSynced = await App.Database.GetItemsNotSynced();
            //    foreach(ComplainItem ci in itemsNotSynced)
            //    {
            //       ci.UpdatedAt = new DateTimeOffset();
            //       await  App.Manager.SaveTaskAsync(ci, true);
            //       await  App.Database.SaveItemAsync(ci);
            //    }

            //    Settings.LastSync = DateTime.Now;
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("Sync Failed:" + ex.Message);
            //}

            return true;
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<ComplainItem>();

            var _serverItems  = await App.ItemsManager.GetItemsAsync();
            var _localItems = await App.ItemsDatabase.Get();

            var _newItems  = _serverItems.Except(_localItems, new IdComparer()).ToList();

            foreach (ComplainItem item in _newItems)
            {
                if (item.UpdatedAt.HasValue)
                {
                    item.UpdatedAt = new DateTimeOffset();
                    await App.ItemsDatabase.Update(item);
                }
                else
                {
                    item.UpdatedAt = new DateTimeOffset();
                    await App.ItemsDatabase.Insert(item);
                }                
            }

            _localItems = await App.ItemsDatabase.Get();

            foreach (ComplainItem item in _localItems)
            {
                items.Add(item);
            }

            isInitialized = true;
        }
    }
}
