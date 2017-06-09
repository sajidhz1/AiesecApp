namespace Aiesec_App.Models
{
    public class Item : BaseDataObject
    {
        public string Name { get; set; }

        public string Notes { get; set; }

        public bool Done { get; set; }

        public string ID { get; internal set; }
    }
}
