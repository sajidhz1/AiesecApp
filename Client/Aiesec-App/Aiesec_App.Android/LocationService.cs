using Android.App;
using Android.Content;
using Aiesec_App.Services;
using Android.Locations;
using Xamarin.Forms;
using Aiesec_App.Droid;
using Application = Android.App.Application;

[assembly: Dependency(typeof(LocationService))]
namespace Aiesec_App.Droid
{
    class LocationService : ILocationService
    {
        Dialog _dialog;

        public bool IsEnabled { get; set; }

        public void CheckServiceEnabled()
        {
            LocationManager locationManager = (LocationManager)Application.Context.GetSystemService(Context.LocationService);

            if (locationManager.IsProviderEnabled(LocationManager.NetworkProvider))
            {
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
            }
        }

        private void showGPSDisabledAlertToUser()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(Application.Context);
            alert.SetTitle("Confirm delete");
            alert.SetMessage("GPS is disabled in your device. Would you like to enable it?");
            alert.SetPositiveButton("Settings", (senderAlert, args) => {
                Intent callGPSSettingIntent = new Intent(
                        Android.Provider.Settings.ActionLocationSourceSettings);
                Application.Context.StartActivity(callGPSSettingIntent);
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                _dialog.Dismiss();
            });

            _dialog = alert.Create();
            _dialog.Show();
        }
    }
}