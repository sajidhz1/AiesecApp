using Aiesec_App.Helpers;
using Aiesec_App.Models;
using Aiesec_App.Services;

using Xamarin.Forms;

namespace Aiesec_App.ViewModels
{
    public class BaseViewModel : ObservableObject
    {       

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }

    public class BaseViewModel<T> : BaseViewModel
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<T> DataStore => DependencyService.Get<IDataStore<T>>();
    }
}

