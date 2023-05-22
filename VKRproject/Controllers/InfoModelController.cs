using Microsoft.AspNetCore.Mvc;
using VKRproject.Models;
using VKRproject.Models.ViewModels;
using VKRproject.Modules;
using VKRproject.Tools;

namespace VKRproject.Controllers
{
    public class InfoModelController : Controller
    {
        LayoutViewModel model = new LayoutViewModel();
        public InfoModelController()
        {
            model.User = AuthorizationModule.AuthUser;
        }
        [HttpGet]
        public IActionResult InfoTour(string id)
        {
            Tour tour = ModelTool.GetTourFromDb(id); 
            return View(tour);
        }
        [HttpGet]
        public IActionResult InfoCountry(int id)
        {
            Country country = ModelTool.GetCountryFromDb(id);
            return View(country);
        }
        [HttpGet]
        public IActionResult InfoCity(int id)
        {
            City city = ModelTool.GetCityFromDb(id);
            return View(city);
        }
        [HttpGet]
        public IActionResult InfoHotel(int id)
        {
            Hotel hotel = ModelTool.GetHotelFromDb(id);
            return View(hotel);
        }
        [HttpGet]
        public IActionResult InfoDepCity(int id)
        {
            DepCity depCity = ModelTool.GetDepCityFromDb(id);
            return View(depCity);
        }
        [HttpGet]
        public IActionResult InfoTourOperator(int id)
        {
            TourOperator tourOperator = ModelTool.GetTourOperatorFromDb(id);
            return View(tourOperator);
        }
        
    }
}
