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
     
            //if (App.IsConnected)
            //{
            //    bool httpStatus  = await App.ItemsManager.SaveTaskAsync(item, true);
            //    if (httpStatus)
            //    {
                    int insertResult = await App.ItemsDatabase.Insert(item);
                    if(insertResult == 1)
                    {
                        items.Add(item);
                    }
            //    }               
                
            //}
            //else
            //{
                
            //}       

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ComplainItem item)
        {

            if (App.IsConnected)
            {
                bool httpStatus = await App.ItemsManager.UpdateTaskAsync(item);
                if (httpStatus)
                {
                    int insertResult = await App.ItemsDatabase.Update(item);
                    if (insertResult == 1)
                    {
                        var _item = items.Where((ComplainItem arg) => arg.ID == item.ID).FirstOrDefault();
                        items.Remove(_item);
                        items.Add(item);
                    }
                }

            }
            else
            {

            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(ComplainItem item)
        {
  
            if (App.IsConnected)
            {
                bool httpStatus = await App.ItemsManager.DeleteTaskAsync(item.ID);
                if (httpStatus)
                {
                    int insertResult = await App.ItemsDatabase.Delete(item);
                    if (insertResult == 1)
                    {
                        var _item = items.Where((ComplainItem arg) => arg.ID == item.ID).FirstOrDefault();
                        items.Remove(_item);
                    }
                }

            }
            else
            {

            }
            return await Task.FromResult(true);
        }

        public async Task<ComplainItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<ComplainItem>> GetItemsAsync(bool forceRefresh = false)
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
            if (isInitialized)
                return;

           
            var _localItems = await App.ItemsDatabase.Get();

            if (_localItems.Count <= 0)
            {
                await InitializeAsync();
                _localItems = await App.ItemsDatabase.Get();
            }

            items = new List<ComplainItem>();
            foreach (ComplainItem item in _localItems)
            {
                items.Add(item);
            }

            isInitialized = true;
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<ComplainItem>();

            var _serverItems  = await App.ItemsManager.GetItemsAsync(Constants.ToDoList);
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

            await SyncAsync();

            isInitialized = true;
        }
    }
}
