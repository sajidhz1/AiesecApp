using Aiesec_App.Data;
using Aiesec_App.Models;
using Aiesec_App.Services;
using Aiesec_App.Views;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Aiesec_App
{
    public partial class App : Application
    {
        static Database<EventItem> _eventsDatabase;
        static Database<ComplainItem> _itemsDatabase;

        static SQLiteAsyncConnection database;

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

        public static Database<EventItem> EventsDatabase
        {
            get
            {
                if (_eventsDatabase == null)
                {
                    _eventsDatabase = new Database<EventItem>(Database);
                }
                return _eventsDatabase;
            }
        }

        public static Database<ComplainItem> ItemsDatabase
        {
            get
            {
                if (_itemsDatabase == null)
                {
                    _itemsDatabase = new Database<ComplainItem>(Database);
                }
                return _itemsDatabase;
            }
        }

        private static SQLiteAsyncConnection Database
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("Aiesec_db.db3"));
                }
                return database;
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
