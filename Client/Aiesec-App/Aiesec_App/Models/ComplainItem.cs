namespace Aiesec_App.Models
{
    public class ComplainItem : BaseDataObject
    {
        public int idComplain { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public int ExchangeParticipant_idExchangeParticipant { get; set; }

        public int Project_idProject {get; set;}

        public string complainStatus { get; set; }

        public int expired { get; set; }
    }
}
