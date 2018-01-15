

using System;

namespace Aiesec_App.Models
{
    public class EventItem : BaseDataObject
    {
        public string title { get; set; }

        public string description { get; set; }

        public string venue { get; set; }

        public int Project_idProject { get; set; }

        public int User_idUser { get; set; }

        public DateTime start { get; set; }

        public DateTime end { get; set; }

        public string eventImage { get; set; }

        public bool expired { get; set; }
    }
}
