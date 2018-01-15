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

            await Navigation.PushAsync(new ItemDetailPage(new ComplainReplyViewModel(item)));

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
        public void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Edit Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public async void OnResolve(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            var item =  mi.CommandParameter as ComplainItem;
            item.complainStatus = ((User)Application.Current.Properties["user"]).userType == 1 ? "1" : "2";
            await viewModel.DataStore.UpdateItemAsync(item);

           
            await DisplayAlert("Resolve Complain", "Complain" + item.title + " is now resolved!", "OK");
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            var item = mi.CommandParameter as ComplainItem;

            //since dulitha thinks that we shouldn't delete anything from database
            //await viewModel.DataStore.DeleteItemAsync(item);
            item.expired = 1;
            await viewModel.DataStore.UpdateItemAsync(item);
            await  DisplayAlert("Delete Complain", "Complain" + item.title + " is now deleted!", "OK");
        }
    }

    class ComplainStatustoColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "2":
                    return Color.Green;
                case "0":
                    return Color.Red;
                case "1":
                    return Color.Orange;
            }

            return Color.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
