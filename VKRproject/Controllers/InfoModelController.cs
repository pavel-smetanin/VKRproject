using Microsoft.AspNetCore.Mvc;
using VKRproject.Models;
using VKRproject.Models.ViewModels;
using VKRproject.Modules;
using VKRproject.Tools;

namespace VKRproject.Controllers
{
    public class InfoModelController : Controller
    {
        InfoModelViewModel model = new InfoModelViewModel();
        public InfoModelController()
        {
            model.User = AuthorizationModule.AuthUser;
        }
        [HttpGet]
        public IActionResult InfoTour(string id)
        {
            model.Tour = ModelTool.GetTourFromDb(id); 
            return View(model);
        }
        [HttpGet]
        public IActionResult InfoCountry(int id)
        {
            model.Country = ModelTool.GetCountryFromDb(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult InfoCity(int id)
        {
            model.City  = ModelTool.GetCityFromDb(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult InfoHotel(int id)
        {
            model.Hotel = ModelTool.GetHotelFromDb(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult InfoDepCity(int id)
        {
            model.DepCity = ModelTool.GetDepCityFromDb(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult InfoTourOperator(int id)
        {
            model.TourOperator = ModelTool.GetTourOperatorFromDb(id);
            return View(model);
        }
        
    }
}
