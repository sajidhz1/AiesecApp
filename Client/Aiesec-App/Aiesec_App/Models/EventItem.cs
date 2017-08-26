using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class EventItem : BaseDataObject
    {
        public string Description { get; set; }

        public string Venue { get; set; }

        public string ProjectID { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public bool Expired { get; set; }
    }
}
