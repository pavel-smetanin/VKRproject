using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKRproject.Models;
using VKRproject.Models.ViewModels;
using VKRproject.Modules;
using VKRproject.Tools;
using System.ComponentModel.DataAnnotations;

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
            Model.AccomsType = ModelTool.GetAccomsType();
            Model.User = AuthorizationModule.AuthUser;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if (Model.User == null)
                return RedirectPermanent(ConfigProvider.Configuration["Auth:ExitUrl"]);
            return View(Model);
        }
        [HttpGet]
        public IActionResult SearchByFilter(Filter filter)
        {
            /*var f = new Filter();
            f.CountryId = 40;
            f.OperatorsId = new List<int> { 9, 38, 54 };
            f.DateLower = "2023-01-01";
            f.DateUpper = "2024-01-01";
            f.PriceLower = 100;
            f.PriceUpper = 100000;
            f.MinNightsCount = 1;
            f.NightsCount = 20;
            f.AdultsCount = 2;
            f.ChildCount = 0;
            f.DepCityId = 832;
            f.MealCode = "AI";
            f.Category = "3*";
            f.Rate = 1.0;
            filter = f;*/
            var context = new ValidationContext(filter);
            var errors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(filter, context, errors))
            {
                string text = "";
                foreach (var e in errors)
                    text += e.ErrorMessage + " \n";
                ViewBag.ValidMessage = text;
                return View("Index", Model);
            }
            SearchModule module = new SearchModule();
            Model.Tours = module.SearchToursByFilter(filter);
            return View("Index", Model);
        }

        [HttpPost]
        public async Task<IActionResult> EmailSend(string email)
        {
            EmailModule module = new EmailModule();
            var tours = Model.Tours;
            //await module.SendToursByEmail(tours, email);
            ViewBag.EmailMessage = "Подборка отправлена на " + email;
            return View(Model);
        }
        public IActionResult Privacy()
        {
            return View(Model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}