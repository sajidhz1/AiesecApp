using Xamarin.Forms;
using Aiesec_App.Droid;
using Aiesec_App.Services;
using System;
using Android.Net;
using Android.Content;
using Application = Android.App.Application;

[assembly: Dependency(typeof(NetworkConnection))]
namespace Aiesec_App.Droid
{
    public class NetworkConnection : INetworkConnection
    {
        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            var connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            if (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting)
            {
                IsConnected = true;
            }
            else
            {
                IsConnected = false;
            }
        }
    }
}