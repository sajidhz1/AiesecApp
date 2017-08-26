using System;
using Aiesec_App.Helpers;
using SQLite;

namespace Aiesec_App.Models
{
    public class BaseDataObject : ObservableObject
    {
        public BaseDataObject()
        {
            ID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Id for item
        /// </summary>
        /// 
        [PrimaryKey]
        public string ID { get; set; }

        /// <summary>
        /// Azure created at time stamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Azure UpdateAt timestamp for online/offline sync
        /// </summary>
        public DateTimeOffset? UpdatedAt { get; set; }

        /// <summary>
        /// Azure version for online/offline sync
        /// </summary>
        public string AzureVersion { get; set; }
    }
}
