using Aiesec_App.Helpers;
using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aiesec_App.ViewModels
{
    public class ItemViewModel : BaseViewModel<Item>
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        //public Command LoadItemsCommand { get; set; }

        public ItemViewModel()
        {
            Title = "Complains";
            Items = new ObservableRangeCollection<Item>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        //async Task ExecuteLoadItemsCommand()
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        Items.Clear();
        //        var items = await DataStore.GetItemsAsync(true);
        //        Items.ReplaceRange(items);
        //    }
        //    catch (Exception ex)
        //    { 
        //        MessagingCenter.Send(new MessagingCenterAlert
        //        {
        //            Title = "Error",
        //            Message = "Unable to load items.",
        //            Cancel = "OK"
        //        }, "message");
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //        var items = await DataStore.SyncAsync();
        //        Items.ReplaceRange(items);
        //    }
        //}
    }
}
