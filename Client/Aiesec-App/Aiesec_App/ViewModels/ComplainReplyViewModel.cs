using Aiesec_App.Helpers;
using Aiesec_App.Models;
using Aiesec_App.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aiesec_App.ViewModels
{
    public class ComplainReplyViewModel : BaseViewModel<ComplainReply>
    {
        public ComplainItem Item { get; set; }
        public ObservableRangeCollection<ComplainReply> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ComplainReplyViewModel(ComplainItem item = null)
        {
            Title = item.title;
            Item = item;

            Items = new ObservableRangeCollection<ComplainReply>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();

                ReplyDataStore rd = (ReplyDataStore)DataStore;
                rd.ComplainItem = Item;

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