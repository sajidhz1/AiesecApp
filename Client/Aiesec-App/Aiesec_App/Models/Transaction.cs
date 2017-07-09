using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    public class Transaction : BaseDataObject  
    {
        public string TransactionType { get; set; }

        public string JsonData { get; set; }

        public string Table { get; set; }
    }

    enum TransactionType
    {
        Insert,
        Update,
        Delete
    }
}
