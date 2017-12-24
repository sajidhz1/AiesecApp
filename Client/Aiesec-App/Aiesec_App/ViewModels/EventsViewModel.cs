﻿using Aiesec_App.Helpers;
using Aiesec_App.Models;
using Aiesec_App.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aiesec_App.ViewModels
{
    class EventsViewModel: BaseViewModel<EventItem>
    {
        public ObservableRangeCollection<EventItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public EventsViewModel()
        {
            Title = "Events";
            Items = new ObservableRangeCollection<EventItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadCommandAsync());
            
            MessagingCenter.Subscribe<NewEventPage, EventItem>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as EventItem;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
        }

        private async Task ExecuteLoadCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to Events.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
