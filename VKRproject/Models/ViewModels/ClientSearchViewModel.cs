using VKRproject.Models;

namespace VKRproject.Models.ViewModels
{
    public class ClientSearchViewModel : LayoutViewModel
    {
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<ShortTour> Tours { get; set; } = new List<ShortTour>();
        public List<ShortTour> ToursArchive { get; set; } = new List<ShortTour>();

    }
}
