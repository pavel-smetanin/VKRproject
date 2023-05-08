using Microsoft.AspNetCore.Mvc;
using VKRproject.Models.ViewModels;
using VKRproject.Models;
using VKRproject.Modules;

namespace VKRproject.Controllers
{
    public class ClientSearchController : Controller
    {
        private ClientSearchModule module = new ClientSearchModule();
        private ClientSearchViewModel model = new ClientSearchViewModel();
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SearchByFio(string lastname, string firstname, string patrname, string birthdate)
        {
            model.Clients = module.SearchClient(lastname, firstname, patrname, birthdate);
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult SearchById(int id)
        {
            model.Clients = module.SearchClient(id);
            return View("Index", model);
        }
        [HttpPost]
        public IActionResult SearchByContact(string email, string phone)
        {
            model.Clients = module.SearchClient(email, phone);
            return View("Index", model);
        }
        [HttpGet]
        public IActionResult Archive(int id)
        {
            SearchModule searchModule = new SearchModule();
            model.ToursArchive = searchModule.SearchToursByClientOrder(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult Result(int id)
        {
            //ViewBag.Message = id;
            var stats = module.GetTourStatsByClientId(id);
            TourStatModule statModule = new TourStatModule();
            var filter = statModule.GetTourStatFilter(stats);
            SearchModule searchModule = new SearchModule();
            model.Tours = searchModule.SearchToursByFilter(filter);
            return View(model);
        }
    }
}
