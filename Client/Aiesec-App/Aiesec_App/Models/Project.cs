using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class Project : BaseDataObject
    {
        public int idProject { get; set; }

        public string projectName { get; set; }

        public string version { get; set; }

        public string shortDescritpion { get; set; }

        public string longDescription { get; set; }

        public string startDate { get; set; }

        public string endDate { get; set; }

        public int LocalCommitte_idLocalCommitte { get; set; }
    }
}
