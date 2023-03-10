using System.Text.Json.Nodes;

namespace VKRproject.Models
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool Visa { get; set; }
        public JsonArray Types { get; set; }
        public JsonObject CurrencyInfo { get; set; }

    }
}
