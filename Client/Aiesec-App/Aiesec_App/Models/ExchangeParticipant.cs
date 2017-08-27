using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class ExchangeParticipant : BaseDataObject
    {
        public int idExchangeParticipant { get; set; }

        public int User_idUser { get; set; }

        public int Country_idCountry { get; set; }

        public int Project_idProject { get; set; }
    }
}
