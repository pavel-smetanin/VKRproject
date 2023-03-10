using System.Text.Json.Nodes;

namespace VKRproject.Models
{
    public class Client : BasePeople
    {
        public DateOnly BirthDate { get; set; }
        public JsonObject Contacts { get; set; }
    }
}
