namespace Aiesec_App.Models
{
    public class ComplainItem : BaseDataObject
    {
        public string Name { get; set; }

        public string Notes { get; set; }

        private bool isDone;
        public bool Done { get { return isDone; }
            set { isDone = value; OnPropertyChanged(); } }
    }
}
