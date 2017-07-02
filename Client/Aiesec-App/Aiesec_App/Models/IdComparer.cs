using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Models
{
    class IdComparer : IEqualityComparer<BaseDataObject>
    {
        public bool Equals(BaseDataObject x1, BaseDataObject x2)
        {
            if (object.ReferenceEquals(x1, x2))
            {
                return true;
            }
            if (object.ReferenceEquals(x1, null) ||
                object.ReferenceEquals(x2, null))
            {
                return false;
            }
            return x1.ID == x2.ID;
        }

        public int GetHashCode(BaseDataObject obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return obj.ID.GetHashCode();
        }
    }
}
