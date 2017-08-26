namespace Aiesec_App.Models
{
    public class ComplainItem : BaseDataObject
    {
        public string Name { get; set; }

        public string Notes { get; set; }

        public int ExchangeParticipant_idExchangeParticipant { get; set; }

        public int Project_idProject {get; set;}

        public string description { get; set; }

        private bool isDone;
        public bool Done { get { return isDone; }
            set { isDone = value; OnPropertyChanged(); } }
    }
}
