using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Web.Models;
using Slobkoll.ERP.Web.Providers.Interface;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult UserIndex()
        {
            var model = _adminProvider.ListToUser();
            return View(model);
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
                    return RedirectToAction("UserIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    return View(model);
                }
            else
            {
                ModelState.AddModelError("", "Ошибка при создание");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult UserEdit(int Id)
        {
            User user = _adminProvider.UserLoad(Id);
            var listen = _adminProvider.ListUser().Where(x => x.Id != user.Id);
            UserEditModel edit = new UserEditModel
            {
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
                Position = user.Position,
                AdminRole = user.AdminRole,
                StatusUser = user.StatusUser
            };
            List<int> listGroup = new List<int>();
            List<int> listPerfomer = new List<int>();
            List<int> listObserved = new List<int>();

            foreach (var item in user.UserPerformer)
            {
                listPerfomer.Add(item.Id);
            }
            ViewBag.UserPerformer = new MultiSelectList(listen, "Id", "Name", listPerfomer);

            foreach (var item in user.Group)
            {
                listGroup.Add(item.Id);
            }
            ViewBag.Group = new MultiSelectList(_adminProvider.ListGroup(), "Id", "Name", listGroup);

            foreach (var item in user.UserObserved)
            {
                listObserved.Add(item.Id);
            }
            ViewBag.UserObserved = new MultiSelectList(listen, "Id", "Name", listObserved);

            return View(edit);
        }
        [HttpPost]
        public ActionResult UserEdit(UserEditModel model)
        {
            if (ModelState.IsValid)
            {
                _adminProvider.UserEdit(model);
                return RedirectToAction("UserIndex");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка введенных данных");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult GroupIndex()
        {
            var model = _adminProvider.ListGroup();
            return View(model);
        }

        [HttpGet]
        public ActionResult GroupCreate()
        {
            ViewBag.User = new SelectList(_adminProvider.ListUser(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult GroupCreate(GroupCreateModel model)
        {
            if (ModelState.IsValid)
                if (_adminProvider.GroupCreate(model))
                {
                    return RedirectToAction("GroupIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    return View(model);
                }
            else
            {
                ModelState.AddModelError("", "Ошибка при создание");
                return View(model);
            }
        }
    }
}