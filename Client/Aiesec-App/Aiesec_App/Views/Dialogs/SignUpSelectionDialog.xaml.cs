using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Aiesec_App.Views.Dialogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpSelectionDialog : ContentPage
    {
        public SignUpSelectionDialog()
        {
            InitializeComponent();
        }

        async void OnSignUpAsEpClicked(object sender, EventArgs args)
		{
             await Navigation.PushModalAsync(new NavigationPage(new SignUpPage()));
        }

        async void OnSignUpAsLCClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LCSignupPage()));
        }
    }
}