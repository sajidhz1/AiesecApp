using Aiesec_App.Data;
using Aiesec_App.Models;
using Aiesec_App.ViewModels;
using Aiesec_App.Views.Dialogs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public SignUpViewModel vm;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = vm = new SignUpViewModel();
            vm.Title = "lala";
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            var signUpSelectionPage = new SignUpSelectionDialog();
            await Navigation.PushModalAsync(signUpSelectionPage);
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            await Login();                      
        }

        async Task Login()
        {
            if (vm.IsBusy)
                return;

            vm.IsBusy = true;

            var user = new User
            {
                username = usernameEntry.Text,
                password = passwordEntry.Text
            };

            var validateFieldsSucceeded = ValidateFields();
            if (validateFieldsSucceeded)
            {
                try
                {
                    var client = new RestClient(Constants.RestUrl);
                    var request = new RestRequest(Constants.URL_SIGNIN, Method.POST);


                    request.AddParameter("email", user.username);
                    request.AddParameter("password", user.password);
                    //request.AddParameter("connection", "{YOUR-CONNECTION-NAME-FOR-USERNAME-PASSWORD-AUTH}");
                    //request.AddParameter("grant_type", "password");
                    //request.AddParameter("scope", "openid");

                    // We execute the request and capture the response
                    // in a variable called `response`
                    IRestResponse response = await client.ExecuteTaskAsync(request);

                    // Using the Newtonsoft.Json library we deserialaize the string into an object,
                    // we have created a LoginToken class that will capture the keys we need
                    LoginToken token = JsonConvert.DeserializeObject<LoginToken>(response.Content);

                    if (token.token != null)
                    {
                        Application.Current.Properties["token"] = token.token;
                        //  Application.Current.Properties["access_token"] = token.access_token;
                        // GetUserData(token.access_token);

                         App.IsUserLoggedIn = true;
                         Navigation.InsertPageBefore(new MainPage(), this);
                         await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Login failed", response.Content, "OK");
                       
                    }
                }
                catch (Exception e)
                {
                  await   DisplayAlert("Error", e.Message, "OK");
          
                }
            }

            passwordEntry.Text = string.Empty;
        }


        public void GetUserData(string token)
        {
            var client = new RestClient("http://10.0.2.2:3000");
            var request = new RestRequest("api/protected/userdetails", Method.GET);
            request.AddParameter("id_token", token);
            request.AddHeader("Authorization", "Bearer " + token);

            IRestResponse response = client.Execute(request);

        }

        private bool ValidateFields()
        {

            if (string.IsNullOrEmpty(usernameEntry.Text))
            {
                DisplayAlert("Incomplete", "Username or Email Required !", "Ok");
                return false;
            }
            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                DisplayAlert("Incomplete", "Password !", "Ok");
                return false;
            }

            return true;
        }

        public class LoginToken
        {
            public string token { get; set; }
            //  public User user { get; set; }
        }

        private void About_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutPage());
        }
    }
}