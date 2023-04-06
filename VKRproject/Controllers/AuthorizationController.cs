using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VKRproject.Models;
using VKRproject.Modules;

namespace VKRproject.Controllers
{
    public class AuthorizationController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Auth()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string login, string password)
        {
            AuthorizationModule authModule = new AuthorizationModule();
            if (authModule.CheckAuth(login, password))
            {
                authModule.InitUser(login, password);
                return RedirectPermanent("Search/Search");
            }
            else
            {
                ViewBag.Message = "Неверный логин или пароль!";
                return View();
            }

        }
    }
}
