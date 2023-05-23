using Microsoft.AspNetCore.Mvc;
using VKRproject.Modules;
using VKRproject.Models.ViewModels;

namespace VKRproject.Controllers
{
    public class AnalyticController : Controller
    {
        private AnalyticModule module = new AnalyticModule();
        private AnalyticViewModel model = new AnalyticViewModel();
        public AnalyticController() 
        {
            model.User = AuthorizationModule.AuthUser;
        }
        public IActionResult Index()
        {
            if(AnalyticModule.DateDataUpdate == null)
            {
                ViewBag.DateUpdate = "Дата и время обновления не определены";
            }
            else
            {
                ViewBag.DateUpdate = AnalyticModule.DateDataUpdate.ToString();
            }
            if(DataUpdaterModule.ErrorFlag)
            {
                ViewBag.UpdateMessage = "При попытке последнего обновления базы произошла ошибка! Данные не обновлены";
            }
            else
            {
                ViewBag.UpdateMessage = $"Данные обновлены: Количество городов: {DataUpdaterModule.CountCities} Количество отелей: {DataUpdaterModule.CountHotels}  Количество туров: {DataUpdaterModule.CountTours} ";
            }
            ViewBag.GeneralToursCount = module.GeneralToursCount();
            ViewBag.GeneralAvgPrice = module.GeneralAvgPrice();
            ViewBag.GeneralAvgNights = module.GeneralAvgNights();
            model.CountriesDict = module.ToursCountsByCountry();
            model.OperatorsDict = module.ToursCountsByOperator();
            model.MaxNightsCount = module.MaxNightsCount();
            model.MinNightsCount = module.MinNightsCount();
            model.MaxPrice = module.MaxPrice();
            model.MinPrice = module.MinPrice();
            model.MaxCountry = module.CountryWithMaxTours();
            model.MaxCity = module.CityWithMaxTours();
            model.MaxHotel = module.HotelWithMaxTours();
            return View(model);
        }
    }
}
