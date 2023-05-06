namespace VKRproject.Models
{
    public class TourStat
    {
        public string TourId { get; set; }
        public int CountryId { get; set; }
        public int NightsCount { get; set; }
        public int AdultsCount { get; set; }
        public int ChildCount { get; set; }
        public int Price { get; set; }
        public float Rate { get; set; }
    }
}
