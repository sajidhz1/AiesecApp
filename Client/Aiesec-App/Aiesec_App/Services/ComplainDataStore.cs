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

            if (App.IsConnected)
            {
                //should use from the app
                item.ExchangeParticipant_idExchangeParticipant = new ExchangeParticipant() {
                    idExchangeParticipant = 1
                };
                item.Project_idProject = new Project() { idProject = 1 , LocalCommitte_idLocalCommitte = item.ExchangeParticipant_idExchangeParticipant.idExchangeParticipant };
                
                bool httpStatus = await App.ItemsManager.SaveTaskAsync(Constants.URL_COMPLAIN, item, true);
                if (httpStatus)
                {
                    //int insertResult = await App.ItemsDatabase.Insert(item);
                    //if (insertResult == 1)
                    //{
                        items.Add(item);
                   // }
                }

            }
            else
            {

            }

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

        public async Task<bool> PullLatestAsync()
        {
            await InitializeAsync();
            return await Task.FromResult(true);
        }


        public async Task<IEnumerable<ComplainItem>> SyncAsync()
        {
            //if (items != null)
            //{
            //    items.Add(new ComplainItem()
            //    {
            //        Name = "Test"
            //        ,
            //        Notes = "Lalaal"
            //    });
            //}
            return await Task.FromResult(items);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            items = new List<ComplainItem>();

            var _serverItems  = await App.ItemsManager.GetItemsAsync(Constants.URL_COMPLAIN);
            //var _localItems = await App.ItemsDatabase.Get();

            //var _newItems  = _serverItems.Except(_localItems, new IdComparer()).ToList();

            //foreach (ComplainItem item in _serverItems)
            //{
            //    if (item.UpdatedAt.HasValue)
            //    {
            //        item.UpdatedAt = new DateTimeOffset();
            //        await App.ItemsDatabase.Update(item);
            //    }
            //    else
            //    {
            //        item.UpdatedAt = new DateTimeOffset();
            //        await App.ItemsDatabase.Insert(item);
            //    }                
            //}

            //_localItems = await App.ItemsDatabase.Get();
            foreach (ComplainItem item in _serverItems)
            {
                items.Add(item);
            }
            

            isInitialized = true;
        }
    }
}
