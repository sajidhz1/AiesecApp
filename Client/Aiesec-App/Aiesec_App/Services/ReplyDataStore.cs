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

    class ReplyDataStore : IDataStore<ComplainReply> , IComplainItem
    {
        bool isInitialized;
        List<ComplainReply> items;

        public ComplainItem ComplainItem { get; set; }

        public async Task<bool> AddItemAsync(ComplainReply item)
        {
            bool httpStatus = await App.ReplyManager.SaveTaskAsync(Constants.URL_REPLY, item, true);
            if (httpStatus)
            {
                //int insertResult = await App.ItemsDatabase.Insert(item);
                //if (insertResult == 1)
                //{
                items.Add(item);
                // }
            }
            return await Task.FromResult(true);
        }

        Task<bool> IDataStore<ComplainReply>.DeleteItemAsync(ComplainReply item)
        {
            throw new NotImplementedException();
        }

        public async Task<ComplainReply> GetItemAsync(string id)
        {
            await SyncAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<ComplainReply>> GetItemsAsync(bool forceRefresh)
        {
            await SyncAsync();

            return await Task.FromResult(items);
        }

        public async Task InitializeAsync()
        {
  
            items = new List<ComplainReply>();

            var _serverItems = await App.ReplyManager.GetItemsAsync(Constants.URL_REPLY+ "?" + nameof(ComplainReply.Complain_idComplain) +"="+ComplainItem.idComplain);
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

            foreach (ComplainReply item in _serverItems)
            {
                items.Add(item);
            }


        }

        Task<bool> IDataStore<ComplainReply>.PullLatestAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ComplainReply>> SyncAsync()
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


        Task<bool> IDataStore<ComplainReply>.UpdateItemAsync(ComplainReply item)
        {
            throw new NotImplementedException();
        }
    }

    public interface IComplainItem
    {
         ComplainItem ComplainItem { get; set; }
    }
}
