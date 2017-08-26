using Aiesec_App.Models;
using Aiesec_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        EventsViewModel viewModel;
        public EventsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new EventsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as EventItem;
            if (item == null)
                return;

            await Navigation.PushAsync(new EventDetailPage(new EventDetailPageViewModel(item)));

            // Manually deselect item
            EventsListView.SelectedItem = null;
        }

        async void AddEvent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewEventPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}