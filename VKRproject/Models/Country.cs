using Newtonsoft.Json.Linq;

namespace VKRproject.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool Visa { get; set; }
        public JArray Types { get; set; }
        public JObject CurrencyInfo { get; set; }

    }
}
