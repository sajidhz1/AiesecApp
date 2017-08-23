using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Services
{
    public interface ILocationService
    {
        bool IsEnabled { get;  }
        void CheckServiceEnabled();
    }
}
