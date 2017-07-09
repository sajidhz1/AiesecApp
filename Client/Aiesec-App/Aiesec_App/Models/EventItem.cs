

namespace Aiesec_App.Models
{
    public class EventItem : BaseDataObject
    {
        public string Description { get; set; }

        public string Venue { get; set; }

        public string ProjectID { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string EventImage { get; set; }

        public bool Expired { get; set; }

        public string Name { get; set; }

        public string Notes { get; set; }
    }
}
