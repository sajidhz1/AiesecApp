using Aiesec_App.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using RestSharp;
using Aiesec_App.Helpers;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Aiesec_App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage, INotifyPropertyChanged
    {
        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return this.isLoading;
            }

            set
            {
                this.isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
                      
            var validateFieldsSucceeded = validateFields();
            if (validateFieldsSucceeded)
            {
                IsLoading = true;

                UserSignup userSignUp =  SignUp(user);

                IsLoading = false;

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

        private bool validateFields() {

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