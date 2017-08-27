namespace Aiesec_App.Models
{
    public class ComplainItem : BaseDataObject
    {
        public string title { get; set; }

        public string description { get; set; }

        public ExchangeParticipant ExchangeParticipant_idExchangeParticipant { get; set; }

        public Project Project_idProject {get; set;}

        private bool isDone;
        public bool Done { get { return isDone; }
            set { isDone = value; OnPropertyChanged(); } }
    }
}
