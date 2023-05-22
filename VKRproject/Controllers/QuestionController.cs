using Microsoft.AspNetCore.Mvc;
using VKRproject.Models;
using VKRproject.Models.ViewModels;
using VKRproject.Modules;

namespace VKRproject.Controllers
{
    public class QuestionController : Controller
    {
        private User AuthUser;
        private static TelegramModule BotModule;
        private SearchViewModel ViewModel = new SearchViewModel();
        public QuestionController()
        {
            ViewModel.User = AuthorizationModule.AuthUser;
        }
        public IActionResult Index()
        {
            return View(ViewModel);  
        }
        public IActionResult Start()
        {
            BotModule = new TelegramModule();
            BotModule.StartBot();
            ViewBag.Code = BotModule.Code;
            //Session = BotModule.Code;
            return View("Index", ViewModel);
        }
        public IActionResult Finish()
        {
            BotModule.FinishBot();
            if (BotModule.Filter.IsFieldFill())
            {
                ViewModel = new SearchViewModel();
                var filter = Filter.FromQuestionFilterToFilter(BotModule.Filter);
                SearchModule module = new SearchModule();
                var tours = module.SearchToursByFilter(filter);
                ViewModel.Tours = tours;
                return View("Index", ViewModel);
            }
            else
            {
                ViewBag.ResultMessage = "Анкета не заполнена";
                return View("Index", ViewModel);
            }
        } 
    }
}  