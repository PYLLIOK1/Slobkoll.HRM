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
        public PartialViewResult ListObserverArchive(int id)
        {
            return PartialView();
        }


        [Authorize]
        [HttpGet]
        public ActionResult AddTask()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.User = new SelectList(_homeProvider.SelectPerfomer(user.Id), "Id", "Name");
            ViewBag.Group = new SelectList(_homeProvider.SelecGroupPerfomer(user.Id), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(TaskCreateModel model)
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.User = new SelectList(_homeProvider.SelectPerfomer(user.Id), "Id", "Name", model.UserIdPerfomers);
            ViewBag.Group = new SelectList(_homeProvider.SelecGroupPerfomer(user.Id), "Id", "Name", model.UserIdPerfomerGroup);
            if (ModelState.IsValid)
            {
                if (model.UserIdPerfomerGroup != null || model.UserIdPerfomers != null)
                {
                    if(model.File != null)
                    {
                        _homeProvider.TaskCreate(model, user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Файл не выбран");
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Не выбран хоть один исполнитель");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }    
        }
    }
}