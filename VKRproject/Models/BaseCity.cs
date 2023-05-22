namespace VKRproject.Models
{
    public abstract class BaseCity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public bool Popular { get; set; }

        public BaseCity()
        {
            Country = new Country();
        }
    }
}
