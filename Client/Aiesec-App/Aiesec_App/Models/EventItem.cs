

namespace Aiesec_App.Models
{
    public class EventItem : BaseDataObject
    {
        public string description { get; set; }

        public string venue { get; set; }

        public int Project_idProject { get; set; }

        public int User_idUser { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public string EventImage { get; set; }

        public bool expired { get; set; }
    }
}
