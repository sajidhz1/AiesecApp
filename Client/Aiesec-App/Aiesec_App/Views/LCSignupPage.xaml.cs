using Aiesec_App.Models;
using Aiesec_App.ViewModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LCSignupPage : ContentPage
    {
        private LcSignUpViewModel vm { get; }

        public LCSignupPage()
        {
            InitializeComponent();
            BindingContext = vm = new LcSignUpViewModel();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await SignUpAsync();
        }

        bool AreDetailsValid(User user)
        {
            return (!string.IsNullOrWhiteSpace(user.username) && !string.IsNullOrWhiteSpace(user.password) && !string.IsNullOrWhiteSpace(user.email) && user.email.Contains("@"));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(vm.Projects.Count == 0)
            {
                vm.LoadItemsCommand.Execute(null);
                userTypeEntry.ItemsSource = vm.UserTypesList;
                projectEntry.ItemsSource = vm.Projects;
            }
        }

        async Task SignUpAsync()
        {
            if (vm.IsBusy)
                return;

            vm.IsBusy = true;

            var user = new User()
            {
                username = usernameEntry.Text,
                password = passwordEntry.Text,
                email = emailEntry.Text,
                userType = vm.SelectedItem.Key,
                LocalCommitte_idLocalCommitte = 1
            };

            var validateFieldsSucceeded = ValidateFields();
            if (validateFieldsSucceeded)
            {
                try
                {

                    //Sign up logic goes here should be moved 
                    var client = new RestClient(Constants.RestUrl);
                    var request = new RestRequest(Constants.URL_SIGNUP, Method.POST);
                    var json = JsonConvert.SerializeObject(user);

                    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
                    request.RequestFormat = DataFormat.Json;

                    //request.AddParameter("email", user.Email);
                    //request.AddParameter("username", user.Username);
                    //request.AddParameter("password", user.Password);
                    //request.AddParameter("userType", 1);
                    //request.AddParameter("LocalCommitte_idLocalCommitte", 1);

                    IRestResponse response = await client.ExecuteTaskAsync(request);
                    UserSignup userSignUp = JsonConvert.DeserializeObject<UserSignup>(response.Content);

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
                        await DisplayAlert("Error", response.Content, "OK");
                    }

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Unable to Signup", "OK");
                }
                finally
                {
                    vm.IsBusy = false;
                }

            }
        }


        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayActionSheet("Action", "Cancel", "Discard", "Do you want to Leave this Form ?");
                if (result.Equals("Discard"))
                {
                    await this.Navigation.PopModalAsync();
                }

            });

            return true;
        }

        private bool ValidateFields()
        {

            if (string.IsNullOrEmpty(firstnameEntry.Text))
            {
                DisplayAlert("Incomplete", "First Name Required !", "Ok");
                return false;
            }
            if (string.IsNullOrEmpty(lastnameEntry.Text))
            {
                DisplayAlert("Incomplete", "Last Name Required !", "Ok");
                return false;
            }
            //if (string.IsNullOrEmpty(countryEntry.))
            //{
            //    DisplayAlert("Incomplete", "E Name Required !", "Ok");
            //    return false;
            //}
            if (string.IsNullOrEmpty(emailEntry.Text))
            {
                DisplayAlert("Incomplete", "Email Required !", "Ok");
                return false;
            }
            if (string.IsNullOrEmpty(usernameEntry.Text))
            {
                DisplayAlert("Incomplete", "Username Required !", "Ok");
                return false;
            }
            if (string.IsNullOrEmpty(passwordEntry.Text))
            {
                DisplayAlert("Incomplete", "Password Required !", "Ok");
                return false;
            }
            if (string.IsNullOrEmpty(confirmPasswordEntry.Text))
            {
                DisplayAlert("Incomplete", "Confirm Password Required !", "Ok");
                return false;
            }
            if (!passwordEntry.Text.Equals(confirmPasswordEntry.Text))
            {
                DisplayAlert("Error", "Password /Confirm Password Does not Match !", "Ok");
                return false;
            }
            if (!validateEmail(emailEntry.Text))
            {
                DisplayAlert("Error", "Invalid Email Address !", "Ok");
                return false;
            }

            return true;
        }

        private bool validateEmail(string email)
        {
            Regex regex = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            Match match = regex.Match(email);

            return match.Success;
        }
    }

}