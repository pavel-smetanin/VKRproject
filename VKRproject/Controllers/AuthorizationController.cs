using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKRproject.Models;
using VKRproject.Models.ViewModels;
using VKRproject.Modules;
using VKRproject.Tools;

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
            return RedirectPermanent(ConfigProvider.Configuration["Auth:ExitUrl"]);
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
                return RedirectPermanent(ConfigProvider.Configuration["Auth:EntryUrl"]);
            }
            else
            {
                ViewBag.Message = "Неверный логин или пароль!";
                return View();
            }
        }
    }
}
