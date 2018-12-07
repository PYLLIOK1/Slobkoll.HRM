using Slobkoll.ERP.Web.Models;
using Slobkoll.ERP.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slobkoll.ERP.Web.Controllers
{
    public class AdminToolsController : Controller
    {
        private readonly IAdminProvider _adminProvider;

        public AdminToolsController(IAdminProvider adminProvider)
        {
            _adminProvider = adminProvider;
        }

        [HttpGet]
        public ActionResult UserCreate()
        {
            ViewBag.User = new SelectList(_adminProvider.ListUser(), "Id", "Name");
            ViewBag.Group = new SelectList(_adminProvider.ListGroup(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult UserCreate(UserCreateModel model)
        {
            if (ModelState.IsValid)
                if (_adminProvider.UserCreate(model))
                {
                    return RedirectToAction("UserCommunication");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    return View(model);
                }
            else
            {
                ModelState.AddModelError("", "Ошибка при регистрации");
                return View(model);
            }
        }
    }
}