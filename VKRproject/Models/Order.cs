namespace VKRproject.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Client Client { get; set; }
        public Tour Tour { get; set; }
        public DateOnly OrderDate { get; set; }
        public float Rate { get; set; }
    }
}
