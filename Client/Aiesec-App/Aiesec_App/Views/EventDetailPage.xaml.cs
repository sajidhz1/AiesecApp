using Aiesec_App.Models;
using Aiesec_App.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : ContentPage
    {
        EventDetailPageViewModel viewModel;

        public EventDetailPage(EventDetailPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        void OnNavigateToLocationClicked(object sender, EventArgs e)
        {
            var uri = new Uri("http://maps.google.com/maps?saddr=" + viewModel.Item.venue.Replace(" ", "+"));
            Device.OpenUri(uri);
        }
    }

    public class EventDetailPageViewModel : BaseViewModel<EventItem>
    {
        public Uri EventImage { get; private set; }

        public EventItem Item { get; set; }

        public EventDetailPageViewModel(EventItem item)
        {
            Item = item;
            EventImage = new Uri(item.EventImage);
        }
    }

}