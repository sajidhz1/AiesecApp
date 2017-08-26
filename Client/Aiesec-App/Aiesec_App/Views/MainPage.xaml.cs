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
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            SetMainPage();
        }

        public void SetMainPage()
        {
            Children.Add(new ItemsPage()
            {
                Title = "Event Schedule",
                Icon = Device.OnPlatform("tab_feed.png", null, null)
            });
            Children.Add(new EventsPage()
            {
                Title = "Complaints and Requests",
                Icon = Device.OnPlatform("tab_about.png", null, null)
            });
            Children.Add(new AboutPage()
            {
                Title = "Survey",
                Icon = Device.OnPlatform("tab_about.png", null, null)
            });
        }

    }
}
