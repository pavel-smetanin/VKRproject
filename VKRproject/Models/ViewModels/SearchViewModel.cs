using VKRproject.Models;

namespace VKRproject.Models.ViewModels
{
    public class SearchViewModel
    {
        public List<Tour> Tours { get; set; } = new List<Tour>();
        public List<Country> Countries { get; set; } = new List<Country>();
    }
}
