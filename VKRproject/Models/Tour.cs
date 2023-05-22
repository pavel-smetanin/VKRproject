using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace VKRproject.Models
{
    public class Tour: ShortTour
    {
        public TourOperator TourOperator { get; set; }
        public Country Country { get; set; }
        public string Room { get; set; }
        public ModelType MealType { get; set; }
        public ModelType AccomType { get; set; }
        public int NightsCount { get; set; }
        public int AdultsCount { get; set; }
        public int ChildCount { get; set; }
        public JArray OpLinks { get; set; }
        public DepCity DepCity { get; set; }
        public Tour()
        {
            TourOperator = new TourOperator();
            Hotel = new Hotel();
            City = new City();
            Country = new Country();
            MealType = new ModelType();
            AccomType = new ModelType();
            DepCity = new DepCity();
        }
    }
}
