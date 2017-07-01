using Aiesec_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiesec_App.Data
{
    class EventsDatabase : IDataBase<EventItem>
    {
        public Task<int> DeleteItemAsync(EventItem item)
        {
            throw new NotImplementedException();
        }

        public Task<List<EventItem>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveItemAsync(EventItem item)
        {
            throw new NotImplementedException();
        }
    }
}
