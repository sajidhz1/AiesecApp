using Aiesec_App.Helpers;
using Aiesec_App.Models;
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

        public UriImageSource EventImage { get; private set; }

        public EventsViewModel()
        {
            Title = "Events";
            Items = new ObservableRangeCollection<EventItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadCommandAsync());
            
            EventImage = new UriImageSource
            {
                Uri = new Uri("https://upload.wikimedia.org/wikipedia/en/5/5f/Original_Doge_meme.jpg")
            };
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
