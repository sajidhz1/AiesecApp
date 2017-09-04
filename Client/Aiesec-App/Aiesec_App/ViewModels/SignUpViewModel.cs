using Aiesec_App.Data;
using Aiesec_App.Helpers;
using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Aiesec_App.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Project> Projects { get; set; }
        public ObservableRangeCollection<Country> Countries { get; set; }

        public static Manager<Country> CountriesManager { get; private set; }
        public static Manager<Project> ProjectsManager { get; private set; }

        public Command LoadItemsCommand { get; set; }

        public SignUpViewModel()
        {
            Title = "SignUp";

            Countries = new ObservableRangeCollection<Country>();
            Projects = new ObservableRangeCollection<Project>();

            CountriesManager = new Manager<Country>(new RestService<Country>());
            ProjectsManager = new Manager<Project>(new RestService<Project>());

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Countries.Clear();
                var countries = await CountriesManager.GetItemsAsync(Constants.URL_COUNTRIES);
                Countries.ReplaceRange(countries);

                Projects.Clear();
                var projects = await ProjectsManager.GetItemsAsync(Constants.URL_PROJECTS);

                Projects.ReplaceRange(projects);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
