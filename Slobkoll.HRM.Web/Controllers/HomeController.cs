using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slobkoll.HRM.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeProvider _homeProvider;
        public HomeController(IHomeProvider homeProvider)
        {
            _homeProvider = homeProvider;
        }
        [Authorize]
        public ActionResult Index()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            return View();
        }
        public PartialViewResult ListAuthor(int id)
        {
            return PartialView();
        }
        public PartialViewResult ListPerfomer(int id)
        {
            return PartialView();
        }
        public PartialViewResult ListObserver(int id)
        {
            return PartialView();
        }
        public PartialViewResult ListArchive(int id)
        {
            return PartialView();
        }



        [HttpGet]
        public ActionResult AddTask()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(TaskCreateModel model)
        {
            return View();
        }
    }
}