using System;

using Aiesec_App.Models;

using Xamarin.Forms;

namespace Aiesec_App.Views
{
    public partial class NewComplainPage : ContentPage
    {
        public ComplainItem Item { get; set; }

        public NewComplainPage()
        {
            InitializeComponent();

            Item = new ComplainItem
            {
                Name = "Item name",
                Notes = "This is a nice description"
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }

    class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return "Complain";

            return (bool)value ? "Complain" : "Request";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}