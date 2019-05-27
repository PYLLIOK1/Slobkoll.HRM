using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slobkoll.HRM.Web.Controllers
{
    public class HelpController : Controller
    {
        private readonly IHomeProvider _homeProvider;
        public HelpController(IHomeProvider homeProvider)
        {
            _homeProvider = homeProvider;
        }
        // GET: Help
        [Authorize]
        public ActionResult Index()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.UserHome = user;
            return View();
        }
        public ActionResult TaskCreatEdit()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.UserHome = user;
            return View();
        }
        public ActionResult Othet()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.UserHome = user;
            return View();
        }
        public ActionResult Admin()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.UserHome = user;
            return View();
        }
    }
}