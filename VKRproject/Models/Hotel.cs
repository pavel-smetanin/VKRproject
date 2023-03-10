namespace VKRproject.Models
{
    public class Hotel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public float Rate { get; set; }
        public City City { get; set; }
    }
}
