using Aiesec_App.Helpers;
using Aiesec_App.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aiesec_App.ViewModels
{
    public class ComplainItemDetailViewModel : BaseViewModel<ReplyItem>
    {
        public ComplainItem Item { get; set; }
        public ObservableRangeCollection<ReplyItem> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ComplainItemDetailViewModel(ComplainItem item = null)
        {
            Title = item.Name;
            Item = item;

            Items = new ObservableRangeCollection<ReplyItem>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
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