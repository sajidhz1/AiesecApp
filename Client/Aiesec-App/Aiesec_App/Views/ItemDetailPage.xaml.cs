
using Aiesec_App.Models;
using Aiesec_App.ViewModels;
using System;
using Xamarin.Forms;

namespace Aiesec_App.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ComplainItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();              
        }
        
        //hack to remove orange highlight
        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        public ItemDetailPage(ComplainItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void Send_Button_Clicked(object sender, System.EventArgs e)
        {
            ReplyItem ri = new ReplyItem()
            {
                Notes = Reply.Text
            };

            if(await viewModel.DataStore.AddItemAsync(ri))
            {
                MessagingCenter.Send(this, "AddItem", ri);
                await Navigation.PopToRootAsync();
            }
            else
            {

            }
        }

        public void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
    }
}
