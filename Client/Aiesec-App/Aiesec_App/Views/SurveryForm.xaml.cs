using Aiesec_App.ViewModels;
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
    public partial class SurveryForm : ContentPage
    {
        SurveyViewModel vm;
        
        public SurveryForm()
        {
            InitializeComponent();
            BindingContext = vm = new SurveyViewModel();
        }
    }
}