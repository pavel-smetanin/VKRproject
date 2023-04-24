using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKRproject.Models;
using VKRproject.Modules;
using VKRproject.Tools;

namespace VKRproject.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private SearchViewModel Model;
        private User AuthUser;

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
            Model = new SearchViewModel();
            Model.Countries = ModelTool.GetCountriesFromDb();
            Model.Tours = new List<Tour>();
            AuthUser = AuthorizationModule.AuthUser;
        }

        [HttpGet]
        public IActionResult Index(Filter filter)
        {
            if (AuthUser == null)
                return RedirectPermanent("~/Authorization/Index");
            ViewBag.UserInfo = $"{AuthUser.Employee.LastName} {AuthUser.Employee.FirstName} {AuthUser.Employee.PatrName} {AuthUser.Employee.Position}";
            SearchModule module = new SearchModule();
            Model.Tours = module.GetToursListByFilter(filter);
            return View(Model);
        }
        /*[HttpGet]
        public IActionResult Index()
        {
            if (AuthUser == null)
                return RedirectPermanent("Authorization/Index");
            return View(Model);
        }*/
        [HttpPost]
        public async Task<IActionResult> Index(string email)
        {
            EmailModule module = new EmailModule();
            var tours = Model.Tours;
            await module.SendToursByEmail(tours, email);
            ViewBag.EmailMessage = "Подборка отправлена на " + email;
            return View(Model);
        }
        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}