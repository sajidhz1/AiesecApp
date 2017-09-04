namespace Aiesec_App.Models
{
    public class ComplainItem : BaseDataObject
    {
        public int idComplain { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public int ExchangeParticipant_idExchangeParticipant { get; set; }

        public int Project_idProject {get; set;}

        private bool isDone;
        public bool Done { get { return isDone; }
            set { isDone = value; OnPropertyChanged(); } }
    }
}
