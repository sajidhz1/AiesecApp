﻿using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Services
{
    public class EventsDataStore : IDataStore<EventItem>
    {
        bool isInitialized;
        List<EventItem> items;

        public async Task<bool> AddItemAsync(EventItem item)
        {
            await InitializeAsync();

            items.Add(item);
            await App.EventsDatabase.Insert(item);
            await App.EventsManager.SaveTaskAsync(item, true);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(EventItem item)
        {
            await InitializeAsync();

            var _item = items.Where((EventItem arg) => arg.ID == item.ID).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<EventItem> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(items.FirstOrDefault(s => s.ID == id));
        }

        public async Task<IEnumerable<EventItem>> GetItemsAsync(bool forceRefresh = false)
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
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateItemAsync(EventItem item)
        {
            await InitializeAsync();

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

            var _serverItems = await App.EventsManager.GetItemsAsync();
            var _localItems = await App.EventsDatabase.Get();

            var _newItems = _serverItems.Except(_localItems, new IdComparer()).ToList();

            foreach (EventItem item in _newItems)
            {
                await App.EventsDatabase.Insert(item);
            }

            _localItems = await App.EventsDatabase.Get();

            foreach (EventItem item in _localItems)
            {
                items.Add(item);
            }

            isInitialized = true;
        }
    }
}