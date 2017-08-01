using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Aiesec_App.Helpers;
using Aiesec_App.Models;
using Aiesec_App.Views;

using Xamarin.Forms;

namespace Aiesec_App.ViewModels
{
    public class ComplainsViewModel : BaseViewModel<ComplainItem>
    {
        public ObservableRangeCollection<ComplainItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ComplainsViewModel()
        {
            Title = "Complains";
            Items = new ObservableRangeCollection<ComplainItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewComplainPage, ComplainItem>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as ComplainItem;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
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
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
                var items = await DataStore.SyncAsync();
                Items.ReplaceRange(items);
            }
        }
    }
}