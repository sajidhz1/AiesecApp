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
    public class LcSignUpViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Project> Projects { get; set; }

        public static Dictionary<int, string> UserTypes =
            new Dictionary<int, string>() { { 1, "Team Lead" }, { 2, "Member" } };

        public static Manager<Project> ProjectsManager { get; private set; }

        public Command LoadItemsCommand { get; set; }


        public List<KeyValuePair<int, string>> UserTypesList
        {
            get => UserTypes.ToList();
        }

        private KeyValuePair<int, string> _selectedItem;
        public KeyValuePair<int, string> SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem = value;
        }

        public LcSignUpViewModel()
        {
            Title = "Member SignUp";

            Projects = new ObservableRangeCollection<Project>();
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
                Projects.Clear();
                var projects = await ProjectsManager.GetItemsAsync(Constants.URL_PROJECTS, false);

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
