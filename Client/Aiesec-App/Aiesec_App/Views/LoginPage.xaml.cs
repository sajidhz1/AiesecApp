using Aiesec_App.Data;
using Aiesec_App.Models;
using Aiesec_App.Views.Dialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var signUpSelectionPage = new SignUpSelectionDialog();
            await Navigation.PushModalAsync(signUpSelectionPage);            
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                username = usernameEntry.Text,
                password = passwordEntry.Text
            };

            var isValid = true; //Login(user);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                passwordEntry.Text = string.Empty;
            }
        }

        public bool Login(User user)
        {
            // We are using the RestSharp library which provides many useful
            // methods and helpers when dealing with REST.
            // We first create the request and add the necessary parameters
            var client = new RestClient("http://10.0.2.2:1337");
            var request = new RestRequest("api/authenticate", Method.POST);
         

            request.AddParameter("email", user.username);
            request.AddParameter("password", user.password);
            //request.AddParameter("connection", "{YOUR-CONNECTION-NAME-FOR-USERNAME-PASSWORD-AUTH}");
            //request.AddParameter("grant_type", "password");
            //request.AddParameter("scope", "openid");

            // We execute the request and capture the response
            // in a variable called `response`
            IRestResponse response = client.Execute(request);

            // Using the Newtonsoft.Json library we deserialaize the string into an object,
            // we have created a LoginToken class that will capture the keys we need
            LoginToken token = JsonConvert.DeserializeObject<LoginToken>(response.Content);

            if (token.token != null)
            {
                Application.Current.Properties["token"] = token.token;
              //  Application.Current.Properties["access_token"] = token.access_token;
               // GetUserData(token.access_token);

                return true;
            }
            else
            {
                DisplayAlert("Login failed", response.Content , "OK");
                return false;
            };
        }


        public void GetUserData(string token)
        {
            var client = new RestClient("http://10.0.2.2:3000");
            var request = new RestRequest("api/protected/userdetails", Method.GET);
            request.AddParameter("id_token", token);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);

         //   User user = JsonConvert.DeserializeObject<User>(response.Content);

            //// Once the call executes, we capture the user data in the
            //// `Application.Current` namespace which is globally available in Xamarin
            //Application.Current.Properties["email"] = user.email;
            //Application.Current.Properties["picture"] = user.picture;

            //// Finally, we navigate the user the the Orders page
            //Navigation.PushModalAsync(new OrdersPage());
        }

        public class LoginToken
        {
            public string token { get; set; }
            public User user { get; set; }
        }

        public class User
        {
            public string idUser { get; set; }
            public string email { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int approved { get; set; }
        }
        private void About_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutPage());
        }
    }
}