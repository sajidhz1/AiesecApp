using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class User : BaseDataObject
    {

        public string email { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public int approved { get; set; }

        public int userType { get; set; }

        public bool expired { get; set; }

        public LocalCommittee LocalCommitte_idLocalCommitte { get; set; }
    }
}
