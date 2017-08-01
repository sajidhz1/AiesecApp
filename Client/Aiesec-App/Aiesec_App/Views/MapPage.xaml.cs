using Aiesec_App.Models;
using Aiesec_App.Views.Controls;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        Geocoder geoCoder;
        Pin pin;

        public MapPage()
        {
            InitializeComponent();
            geoCoder = new Geocoder();

            SetupCameraInitially();
            MessagingCenter.Subscribe<AddressSelectionPage, Item>(this, "SelectedItem", async (obj, item) =>
            {
                map.Pins.Clear();

                var _item = item as Item;
                pinDisplay.Text = item.Description;
                var approximateLocations = await geoCoder.GetPositionsForAddressAsync(_item.Description);
                foreach (var position in approximateLocations)
                {
                    var pinposition = new Position(position.Latitude, position.Longitude);
                    await map.MoveCamera(CameraUpdateFactory.NewPositionZoom(pinposition, 15d));

                    pin.Position = pinposition;
                    pin.Address = _item.Description;
                }
                map.Pins.Add(pin);
            });
        }

        async void PinLocation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddressSelectionPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected void UpdateCamera() { }

        private async void SetupCameraInitially()
        {

            await Task.Delay(1000); // workaround for #30 [Android]Map.Pins.Add doesn't work when page OnAppearing

            var locator = CrossGeolocator.Current;
            var position = await CrossGeolocator.Current.GetLastKnownLocationAsync();
            if (position == null)
                position = await locator.GetPositionAsync(System.TimeSpan.FromMilliseconds(10000));

            var lastKnownLocation = new Position(position.Latitude, position.Longitude);
            map.InitialCameraUpdate = CameraUpdateFactory.NewPositionZoom(lastKnownLocation, 15d);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(lastKnownLocation, Distance.FromMiles(1)));

            pin = new Pin()
            {
                Type = PinType.Place,
                Label = "Tokyo SKYTREE",
                Address = "Sumida-ku, Tokyo, Japan",
                Position = lastKnownLocation,
                Icon = BitmapDescriptorFactory.FromView(new BindingPinView(pinDisplay.Text))
            };
            pin.IsDraggable = true;
            map.Pins.Add(pin);
        }
    }
}