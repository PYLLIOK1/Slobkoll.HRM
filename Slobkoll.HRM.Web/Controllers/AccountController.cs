using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slobkoll.HRM.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthProvider _authProvider;

        public AccountController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        public ActionResult Login()
        {
            if (_authProvider.IsLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {

            if (ModelState.IsValid && _authProvider.Login(model))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                return View(model);
            }
        }
        public ActionResult Logoff()
        {
            _authProvider.Logout();
            return RedirectToAction("Login", "Account");
        }
    }
}