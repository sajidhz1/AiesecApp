using Aiesec_App.Data;
using Aiesec_App.Models;
using Aiesec_App.Services;
using Aiesec_App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Aiesec_App
{
    public partial class App : Application
    {
        public static Manager<ComplainItem> ItemsManager { get; private set; }
        public static Manager<EventItem> EventsManager { get; private set; }
        public static Manager<ComplainReply> ReplyManager { get; private set; }

        public static bool IsUserLoggedIn { get; set; }

        static INetworkConnection networkConnection;

        public App()
        {
            InitializeComponent();

            networkConnection = DependencyService.Get<INetworkConnection>();
            ItemsManager = new Manager<ComplainItem>(new RestService<ComplainItem>());
            EventsManager = new Manager<EventItem>(new RestService<EventItem>());
            ReplyManager = new Manager<ComplainReply>(new RestService<ComplainReply>());

            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (!IsUserLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                Current.MainPage = new NavigationPage(new MainPage());
            }
        }

        public static bool IsConnected
        {
            get
            {
                networkConnection.CheckNetworkConnection();
                return networkConnection.IsConnected;
            }          
        }
    }
}
