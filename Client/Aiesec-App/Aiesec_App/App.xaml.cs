using Aiesec_App.Data;
using Aiesec_App.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Aiesec_App
{
    public partial class App : Application
    {
        static ItemDataBase database;

        public static ItemManager Manager { get; private set; }

        public static bool IsUserLoggedIn { get; set; }

        public App()
        {
            InitializeComponent();

            Manager = new ItemManager(new RestService());
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

        public static ItemDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ItemDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return database;
            }
        }
    }
}
