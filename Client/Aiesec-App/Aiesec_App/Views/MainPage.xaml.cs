using System;
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

            ToolbarItems.Add(new ToolbarItem("Sign Out" , null, async () =>
            {
                Application.Current.Properties["token"] = null;
                Application.Current.Properties["user"] = null;

                App.IsUserLoggedIn = false;
                App.Current.MainPage = new NavigationPage(new LoginPage());
                await Navigation.PopAsync();

            }, ToolbarItemOrder.Secondary));
        }

        public void SetMainPage()
        {
            Children.Add(new ItemsPage()
            {
                Title = "Complaints and Requests",
                Icon = Device.OnPlatform("tab_feed.png", null, null)
            });
            Children.Add(new EventsPage()
            {
                Title = "Event Schedule",
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
