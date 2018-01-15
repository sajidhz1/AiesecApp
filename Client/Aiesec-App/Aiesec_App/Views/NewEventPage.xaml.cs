﻿using Aiesec_App.Models;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEventPage : ContentPage
    {
        MediaFile file;
        public NewEventPage()
        {
            InitializeComponent();
            Title = "New Event";
            MessagingCenter.Subscribe<MapPage, Item>(this, "OnBackPressed", (obj, item) =>
            {
                var _item = item as Item;
                Location.Text = item.Description;
            });

            BindingContext = this;
        }

        async void OnAddPhotoClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });


            if (file == null)
                return;

            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

        }

        async void OnPickLocationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await AddNewEvent();
        }

        async Task AddNewEvent()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            EventItem ri = new EventItem()
            {   
                title = titleText.Text,
                venue = Location.Text,
                start = DateTime.Now + StartTime.Time,
                end = DateTime.Now + EndTime.Time ,
                description = Description.Text,
                expired = false,
                User_idUser = ((User)Application.Current.Properties["user"]).idUser,
                Project_idProject = ((Project)Application.Current.Properties["project"]).idProject
            };


            var client = new RestClient(Constants.CLOUDINARY_URL);
            var request = new RestRequest("", Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("api_key", "474249572247384");
            request.AddParameter("timestamp", new DateTime().ToString("yyyyMMddHHmmssffff"));
            request.AddParameter("upload_preset", "v1akpj8j");
            byte[] data = ReadFully(file);
            request.AddFileBytes("file", data, "Test", "multipart/form-data");
            try
            {

                IRestResponse response = client.Execute(request);
                IsBusy = true;
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    CloudinaryResponse obj = JsonConvert.DeserializeObject<CloudinaryResponse>(response.Content);
                    ri.eventImage = obj.url;                   
                }

            }
            catch (Exception ex)
            {               
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            finally
            {
                IsBusy = false;
                MessagingCenter.Send(this, "AddItem", ri);
                await Navigation.PopToRootAsync();                
            }
        }

        public static byte[] ReadFully(MediaFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                return memoryStream.ToArray();
            }
        }

    }

    class CloudinaryResponse
    {
       public  string url { get; set; }
    }
}