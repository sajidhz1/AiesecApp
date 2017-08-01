using System;

using Aiesec_App.Models;
using Aiesec_App.ViewModels;

using Xamarin.Forms;

namespace Aiesec_App.Views
{
    public partial class ItemsPage : ContentPage
    {
        ComplainsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ComplainsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ComplainItem;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ComplainItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewComplainPage());
        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
