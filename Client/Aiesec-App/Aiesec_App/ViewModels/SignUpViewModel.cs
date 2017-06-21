using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        public SignUpViewModel()
        {
            Title = "About";           
        }

        bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }
    }
}
