using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class LocalCommittee : BaseDataObject
    {
        public int idLocalCommitte { get; set; }
        public string lcCode { get; set; }

        public string officialAddress { get; set; }

        public string contactNumber { get; set; }

        public string shortDescription{ get; set; }
}
}
