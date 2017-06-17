using Aiesec_App.Data;
using Aiesec_App.Models;
using Newtonsoft.Json;
using RestSharp;
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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var isValid = Login(user);
            if (isValid)
            {
                App.IsUserLoggedIn = true;
               Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }

        public bool Login(User user)
        {
            // We are using the RestSharp library which provides many useful
            // methods and helpers when dealing with REST.
            // We first create the request and add the necessary parameters
            var client = new RestClient("http://10.0.2.2:3000");
            var request = new RestRequest("api/authenticate", Method.POST);
            request.AddParameter("client_id", "{YOUR-AUTH0-CLIENT-ID");
            request.AddParameter("email", user.Username);
            request.AddParameter("password", user.Password);
            //request.AddParameter("connection", "{YOUR-CONNECTION-NAME-FOR-USERNAME-PASSWORD-AUTH}");
            //request.AddParameter("grant_type", "password");
            //request.AddParameter("scope", "openid");

            // We execute the request and capture the response
            // in a variable called `response`
            IRestResponse response = client.Execute(request);

            // Using the Newtonsoft.Json library we deserialaize the string into an object,
            // we have created a LoginToken class that will capture the keys we need
            LoginToken token = JsonConvert.DeserializeObject<LoginToken>(response.Content);

            if (token.id_token != null)
            {
                Application.Current.Properties["id_token"] = token.id_token;
                Application.Current.Properties["access_token"] = token.access_token;
                //GetUserData(token.id_token);

                return true;
            }
            else
            {
                DisplayAlert("Oh No!", response.Content , "OK");

                return false;
            };
        }


        public void GetUserData(string token)
        {
            var client = new RestClient("https://{YOUR-AUTH0-DOMAIN}.auth0.com");
            var request = new RestRequest("tokeninfo", Method.GET);
            request.AddParameter("id_token", token);


            IRestResponse response = client.Execute(request);

            User user = JsonConvert.DeserializeObject<User>(response.Content);

            //// Once the call executes, we capture the user data in the
            //// `Application.Current` namespace which is globally available in Xamarin
            //Application.Current.Properties["email"] = user.email;
            //Application.Current.Properties["picture"] = user.picture;

            //// Finally, we navigate the user the the Orders page
            //Navigation.PushModalAsync(new OrdersPage());
        }

        public class LoginToken
        {
            public string id_token { get; set; }
            public string access_token { get; set; }
            public string token_type { get; set; }
        }

    }
}