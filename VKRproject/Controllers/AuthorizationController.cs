using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKRproject.Models;
using VKRproject.Models.ViewModels;
using VKRproject.Modules;

namespace VKRproject.Controllers
{
    public class AuthorizationController : Controller
    {
        LayoutViewModel model = new LayoutViewModel();
        public AuthorizationController()
        {
            model.User = AuthorizationModule.AuthUser;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Auth()
        {
            return View();
        }
        public IActionResult Exit()
        {
            AuthorizationModule.ClearUser();
            return RedirectPermanent("~/Authorization/Index");
        }
        public IActionResult InfoUser()
        {
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(string login, string password)
        {
            AuthorizationModule authModule = new AuthorizationModule();
            if (authModule.CheckAuth(login, password))
            {
                authModule.InitUser(login, password);
                return RedirectPermanent("~/Search/Index");
            }
            else
            {
                ViewBag.Message = "Неверный логин или пароль!";
                return View();
            }
        }
    }
}
