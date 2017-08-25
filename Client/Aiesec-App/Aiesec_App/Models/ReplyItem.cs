using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class ReplyItem : BaseDataObject
    {
        public string Description { get; set; }

        public string Notes { get; set; } //change to user id

        private bool isDone;
        public bool Done  // change to date, complainid
        {
            get { return isDone; }
            set { isDone = value; OnPropertyChanged(); }
        }
    }
}
