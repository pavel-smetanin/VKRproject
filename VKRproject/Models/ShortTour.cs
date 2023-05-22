using VKRproject.Models;
namespace VKRproject.Models
{
    public class ShortTour
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateFinish { get; set; }
        public Hotel Hotel { get; set; }
        public City City { get; set; }
        public int Price { get; set; }
        public string ImgLink { get; set; }
    }
}
