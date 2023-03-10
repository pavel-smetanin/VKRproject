using System.Text.Json.Nodes;

namespace VKRproject.Models
{
    public class Tour
    {
        public string ID { get; set; }
        public TourOperator TourOperator { get; set; }
        public Hotel Hotel { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public ModelType MealType { get; set; }
        public ModelType AccomType { get; set; }
        public DateOnly DateStart { get; set; }
        public DateOnly DateFinish { get; set; }
        public int NightsCount { get; set; }
        public int AdultsCount { get; set; }
        public int ChildCount { get; set; }
        public JsonArray OpLinks { get; set; }
        public string ImgLink { get; set; }
        public DepCity DepCity { get; set; }
        public int Price { get; set; }
    }
}
