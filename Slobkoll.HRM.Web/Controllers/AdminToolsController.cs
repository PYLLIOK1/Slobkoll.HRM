using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Slobkoll.HRM.Web.Controllers
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
            List<int> listPerfomerGroup = new List<int>();

            foreach (var item in user.GroupPerformer)
            {
                listPerfomerGroup.Add(item.Id);
            }
            ViewBag.UserGroupPerformer = new MultiSelectList(_adminProvider.ListGroup(), "Id", "Name", listPerfomerGroup.ToArray());

            foreach (var item in user.UserPerformer)
            {
                listPerfomer.Add(item.Id);
            }
            ViewBag.UserPerformer = new MultiSelectList(listen, "Id", "Name", listPerfomer.ToArray());

            foreach (var item in user.Group)
            {
                listGroup.Add(item.Id);
            }
            ViewBag.Group = new MultiSelectList(_adminProvider.ListGroup(), "Id", "Name", listGroup.ToArray());

            foreach (var item in user.UserObserved)
            {
                listObserved.Add(item.Id);
            }
            ViewBag.UserObserved = new MultiSelectList(listen, "Id", "Name", listObserved.ToArray());

            return View(edit);
        }
        [HttpPost]
        public ActionResult UserEdit(UserEditModel model)
        {
            if (ModelState.IsValid && _adminProvider.UserEdit(model))
            {
                return RedirectToAction("UserIndex");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка введенных данных");
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult UserDetails(int Id)
        {
            var user = _adminProvider.UserLoad(Id);
            UserDetailModel model = new UserDetailModel
            {
                Id = Id,
                Name = user.Name,
                Login = user.Login,
                Position = user.Position,
                Group = user.Group,
                UserCustomer = user.UserCustomer,
                UserObserved = user.UserObserved,
                UserObserver = user.UserObserver,
                UserPerfomer = user.UserPerformer,
                UserIdPerfomerGroup = user.GroupPerformer
            };
            return View(model);
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
        [HttpGet]
        public ActionResult GroupEdit(int Id)
        {
            Group group = _adminProvider.GroupLoad(Id);
            GroupEditModel edit = new GroupEditModel
            {
                Name = group.Name
            };
            List<int> listUser = new List<int>();
            foreach (var item in group.User)
            {
                listUser.Add(item.Id);
            }
            ViewBag.Group = new MultiSelectList(_adminProvider.ListUser(), "Id", "Name", listUser.ToArray());

            return View(edit);
        }
        [HttpPost]
        public ActionResult GroupEdit(GroupEditModel model)
        {
            if (ModelState.IsValid && _adminProvider.GroupEdit(model))
            {
                return RedirectToAction("GroupIndex");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка введенных данных");
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult GroupDelete(int Id)
        {
            Group group = _adminProvider.GroupLoad(Id);
            GroupDeleteModel delete = new GroupDeleteModel
            {
                Name = group.Name
            };
            return View(delete);
        }
        [HttpPost]
        public ActionResult GroupDelete(GroupDeleteModel model)
        {
            if (ModelState.IsValid)
            {
                _adminProvider.GroupDelete(model);
                return RedirectToAction("GroupIndex");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка введенных данных");
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult GroupDetails(int Id)
        {
            var group = _adminProvider.GroupLoad(Id);
            GroupDetailsModel model = new GroupDetailsModel
            {
                Id = Id,
                Name = group.Name,
                GroupUser = group.User
            };
            return View(model);
        }
    }
}

