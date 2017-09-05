﻿using Aiesec_App.Data;
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
        RestClient client;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = vm = new SignUpViewModel();
            client = new RestClient(Constants.RestUrl);
            Title = "Login";
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
                    var request = new RestRequest(Constants.URL_SIGNIN, Method.POST);

                    request.AddParameter("email", user.username);
                    request.AddParameter("password", user.password);

                    // We execute the request and capture the response
                    // in a variable called `response`
                    IRestResponse response = await client.ExecuteTaskAsync(request);

                    // Using the Newtonsoft.Json library we deserialaize the string into an object,
                    // we have created a LoginToken class that will capture the keys we need
                    LoginToken login = JsonConvert.DeserializeObject<LoginToken>(response.Content);

                    if (login.token != null)
                    {
                        Application.Current.Properties["token"] = login.token;
                        Application.Current.Properties["user"] = login.user;

                        App.IsUserLoggedIn = true;
                        Navigation.InsertPageBefore(new MainPage(), this);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        vm.IsBusy = false;
                        await DisplayAlert("Login failed", response.Content, "OK");
                    }
                }
                catch (Exception e)
                {
                    vm.IsBusy = false;
                    await DisplayAlert("Error", e.Message, "OK");
                }
            }

            passwordEntry.Text = string.Empty;
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

            public User user { get; set; }
        }

        private void About_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutPage());
        }
    }
}