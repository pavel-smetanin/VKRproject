using VKRproject.Models;

namespace VKRproject.Models.ViewModels
{
    public class SearchViewModel : LayoutViewModel
    {
        public List<ShortTour> Tours { get; set; } = new List<ShortTour>();
        public List<ShortTour> ShortTours { get; set; } = new List<ShortTour>();
        public Dictionary<int, string> CountriesDict { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> OperatorsDict { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> DepCitiesDict { get; set; } = new Dictionary<int, string>();
        public List<ModelType> MealsType { get; set; } = new List<ModelType>();
    }
}
