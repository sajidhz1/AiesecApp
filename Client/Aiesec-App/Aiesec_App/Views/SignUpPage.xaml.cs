using Aiesec_App.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Aiesec_App.ViewModels;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        private SignUpViewModel vm { get; }

        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = vm = new SignUpViewModel();
        }

        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await SignUpAsync();
        }

        bool AreDetailsValid(User user)
        {
            return (!string.IsNullOrWhiteSpace(user.Username) && !string.IsNullOrWhiteSpace(user.Password) && !string.IsNullOrWhiteSpace(user.Email) && user.Email.Contains("@"));
        }

        async Task SignUpAsync()
        {
            if (vm.IsBusy)
                return;

            vm.IsBusy = true;

            var user = new User()
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text,
                Email = emailEntry.Text
            };

            var validateFieldsSucceeded = ValidateFields();
            if (validateFieldsSucceeded)
            {
                try
                {

                    //Sign up logic goes here should be moved 
                    var client = new RestClient("http://10.0.2.2:1337");
                    var request = new RestRequest("auth/signup", Method.POST);
                    request.AddParameter("email", user.Email);
                    request.AddParameter("username", user.Username);
                    request.AddParameter("password", user.Password);
                    request.AddParameter("userType", 1);
                    request.AddParameter("LocalCommitte_idLocalCommitte", 1);

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
            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayActionSheet("Action", "Cancel", "Discard", "Do you want to Leave this Form ?");
                if (result.Equals("Discard"))
                {
                    await this.Navigation.PopModalAsync();
                }
               
             });

            return true;
        }

        private bool ValidateFields() {

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

    public class UserSignup
    {
        public string user_id { get; set; }
    }
}