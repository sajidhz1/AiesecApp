using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class ComplainReply : BaseDataObject
    {
        public int idComplainReply { get; set; }

        public int Complain_idComplain { get; set; }

        public int User_idUser { get; set; }

        public string replyText { get; set; }

        public string name { get; set; }

    }
}
