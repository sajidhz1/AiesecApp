using Aiesec_App.Models;
using Aiesec_App.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressSelectionPage : ContentPage
    {
        public AddressSelectionPage()
        {
            InitializeComponent();
            geoCoder = new Geocoder();
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            BindingContext = viewModel = new ItemViewModel();
        }

        ItemViewModel viewModel;
        Geocoder geoCoder;
        HttpClient client;

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            MessagingCenter.Send(this, "SelectedItem", item);

            // Manually deselect item
            ItemsListView.SelectedItem = null;
            await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);

            pinDisplay.TextChanged += async (sender, e) =>
            {
                viewModel.Items.Clear();

                string url = string.Format("https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&components=country:lk&types=geocode||establishment&key=AIzaSyCIDaRb9Nq5jfC1IK7XOcFOICJ9UzkonZw", e.NewTextValue);
                var uri = new Uri(url);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var newlist = JsonConvert.DeserializeObject<JsonModel>(content);

                    if (newlist.predictions.Count > 0)
                        viewModel.Items.AddRange(newlist.predictions);
                }
            };
        }
    }

    public class JsonModel
    {
        [JsonProperty("predictions")]
        public List<Item> predictions { get; set; }

        [JsonProperty("status")]
        public string statuc { get; set; }
    }
}
