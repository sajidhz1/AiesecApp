using Aiesec_App.Models;

namespace Aiesec_App.ViewModels
{
    public class ComplainItemDetailViewModel : BaseViewModel<ComplainItem>
    {
        public ComplainItem Item { get; set; }
        public ComplainItemDetailViewModel(ComplainItem item = null)
        {
            Title = item.Name;
            Item = item;
        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }
    }
}