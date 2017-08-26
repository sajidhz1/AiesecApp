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
    }

    public class EventDetailPageViewModel : BaseViewModel<EventItem>
    {
        public Uri EventImage { get; private set; }

        private EventItem Item { get; set; }

        public EventDetailPageViewModel(EventItem item)
        {
            this.Item = item;

            EventImage = new Uri(item.EventImage);
        }
    }
}