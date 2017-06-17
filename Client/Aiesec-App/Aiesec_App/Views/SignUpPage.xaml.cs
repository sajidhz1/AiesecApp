using Aiesec_App.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using RestSharp;
using Aiesec_App.Helpers;
using System.Threading.Tasks;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var user = new User()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text
            };
                      
            var signUpSucceeded = AreDetailsValid(user);
            if (signUpSucceeded)
            {
                UserSignup userSignUp =  SignUp(user);

                if (!string.IsNullOrEmpty(userSignUp.user_id))
                {
                    var rootPage = Navigation.NavigationStack.FirstOrDefault();
                    if (rootPage != null)
                    {
                        App.IsUserLoggedIn = true;
                        Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack.First());
                        await Navigation.PopToRootAsync();
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Unable to Signup", "OK");                    
                }
            }
        }

        bool AreDetailsValid(User user)
        {
            return (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"));
        }

        UserSignup SignUp(User user)
        {
            //Sign up logic goes here should be moved 
            var client = new RestClient("http://10.0.2.2:3000");
            var request = new RestRequest("api/users", Method.POST);
            request.AddParameter("email", user.Username);
            request.AddParameter("password", user.Password);

            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<UserSignup>(response.Content);
        }
    }

    public class UserSignup
    {
        public string user_id { get; set; }
    }
}