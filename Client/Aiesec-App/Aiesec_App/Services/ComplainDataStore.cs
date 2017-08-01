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
            //isInitialized = forceRefresh;

            await InitializeAsync();
            return await Task.FromResult(items);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        public async Task<IEnumerable<ComplainItem>> SyncAsync()
        {
            if (items != null)
            {
                items.Add(new ComplainItem()
                {
                    Name = "Test"
                    ,
                    Notes = "Lalaal"
                });
            }
            return await Task.FromResult(items);
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

            _localItems = await App.ItemsDatabase.Get();
            foreach (ComplainItem item in _localItems)
            {
                items.Add(item);
            }
            

            isInitialized = true;
        }
    }
}
