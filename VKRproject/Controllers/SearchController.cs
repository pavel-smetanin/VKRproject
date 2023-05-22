using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKRproject.Models;
using VKRproject.Models.ViewModels;
using VKRproject.Modules;
using VKRproject.Tools;

namespace VKRproject.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private SearchViewModel Model = new SearchViewModel();

        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
            Model.CountriesDict = ModelTool.GetCountriesDict();
            Model.Tours = new List<ShortTour>();
            Model.OperatorsDict = ModelTool.GetOperatorsDict();
            Model.DepCitiesDict = ModelTool.GetDepCitiesDict();
            Model.MealsType = ModelTool.GetMealsType();
            Model.User = AuthorizationModule.AuthUser;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (Model.User == null)
                return RedirectPermanent("~/Authorization/Index");
            return View(Model);
        }
        [HttpGet]
        public IActionResult SearchByFilter(Filter filter)
        {
            SearchModule module = new SearchModule();
            Model.Tours = module.SearchToursByFilter(filter);
            return View("Index", Model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string email)
        {
            EmailModule module = new EmailModule();
            var tours = Model.Tours;
            //await module.SendToursByEmail(tours, email);
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