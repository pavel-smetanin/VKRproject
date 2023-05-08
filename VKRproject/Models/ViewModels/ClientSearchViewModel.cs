using VKRproject.Models;

namespace VKRproject.Models.ViewModels
{
    public class ClientSearchViewModel
    {
        public List<Client> Clients { get; set; }
        public List<Tour> Tours { get; set; }
        public List<Tour> ToursArchive { get; set; }

    }
}
