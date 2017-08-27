using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(Aiesec_App.Services.ReplyDataStore))]
namespace Aiesec_App.Services
{
   
    class ReplyDataStore :  IDataStore<ReplyItem>
    {
        bool isInitialized;
        List<ReplyItem> items;

        Task<bool> IDataStore<ReplyItem>.AddItemAsync(ReplyItem item)
        {
            throw new NotImplementedException();
        }

        Task<bool> IDataStore<ReplyItem>.DeleteItemAsync(ReplyItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<ReplyItem> GetItemAsync(string id)
        {
            await SyncAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<ReplyItem>> GetItemsAsync(bool forceRefresh)
        {
            await SyncAsync();

            return await Task.FromResult(items);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<ReplyItem>();

            var _serverItems = await App.ReplyManager.GetItemsAsync(Constants.URL_COMPLAIN);
            var _localItems = await App.EventsDatabase.Get();

            var _insertItems = _serverItems.Except(_localItems, new IdComparer()).ToList();
            //foreach (ReplyItem item in _insertItems)
            //{
            //    await App.EventsDatabase.Insert(item);
            //}

            //var _updateItems = _serverItems.Except(_insertItems, new IdComparer()).ToList();
            //foreach (ReplyItem item in _updateItems)
            //{
            //    await App.EventsDatabase.Update(item);
            //}

            //_localItems = await App.EventsDatabase.Get();

            foreach (ReplyItem item in _serverItems)
            {
                items.Add(item);
            }

            isInitialized = true;
        }

        Task<bool> IDataStore<ReplyItem>.PullLatestAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ReplyItem>> SyncAsync()
        {
            //items = new List<ReplyItem>();
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


        Task<bool> IDataStore<ReplyItem>.UpdateItemAsync(ReplyItem item)
        {
            throw new NotImplementedException();
        }
    }
}
