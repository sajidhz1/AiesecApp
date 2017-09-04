using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class Country : BaseDataObject
    {
        public int idCountry { get; set; }

        public string name { get; set; }

        public string countryCode { get; set; }

        public string description { get; set; }

       // public string locationCoordinates { get; set; }
    }
}
