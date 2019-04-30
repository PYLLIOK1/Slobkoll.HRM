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
            if (_adminProvider.UserAdmin(User.Identity.Name))
            {
                var model = _adminProvider.ListToUser();
                return View(model.Reverse());
            }
            else
            {
                return Error();
            }
            
        }
        [HttpGet]
        public ActionResult UserCreate()
        {
            if (_adminProvider.UserAdmin(User.Identity.Name))
            {
                ViewBag.UserIdPerfomers = new MultiSelectList(_adminProvider.ListUser(), "Id", "Name");
                ViewBag.UserIdCustomer = ViewBag.UserIdPerfomers;
                ViewBag.UserIdObserver = ViewBag.UserIdPerfomers;
                ViewBag.UserIdObserved = ViewBag.UserIdPerfomers;
                ViewBag.UserIdCustomerGroup = new MultiSelectList(_adminProvider.ListGroup(), "Id", "Name");
                ViewBag.UserIdPerfomerGroup = ViewBag.UserIdCustomerGroup;
                ViewBag.IdGroup = ViewBag.UserIdCustomerGroup;
                return View();
            }
            else
            {
                return Error();
            }
            
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
                    return RedirectToAction("UserCreate");
                }
            else
            {
                return RedirectToAction("UserCreate");
            }
        }
        [HttpGet]
        public ActionResult UserEdit(int Id)
        {
            if (_adminProvider.UserAdmin(User.Identity.Name))
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
            else
            {
                return Error();
            }  
        }
        [HttpPost]
        public ActionResult UserEdit(UserEditModel model)
        {
            if (ModelState.IsValid && _adminProvider.UserEdit(model))
            {
                return UserIndex();
            }
            else
            {
                ModelState.AddModelError("", "Ошибка введенных данных");
                return RedirectToAction("UserEdit", model.Id);
            }
        }
        [HttpGet]
        public ActionResult UserDetails(int Id)
        {
            if (_adminProvider.UserAdmin(User.Identity.Name))
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
            else
            {
                return Error();
            }
           
        }


        [HttpGet]
        public ActionResult GroupIndex()
        {
            if (_adminProvider.UserAdmin(User.Identity.Name))
            {
                var model = _adminProvider.ListGroup();
                return View(model.Reverse());
            }
            else
            {
                return Error();
            }
            
        }
        [HttpGet]
        public ActionResult GroupCreate()
        {
            if (_adminProvider.UserAdmin(User.Identity.Name))
            {
                ViewBag.User = new SelectList(_adminProvider.ListUser(), "Id", "Name");
                return View();
            }
            else
            {
                return Error();
            }
            
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
                    return RedirectToAction("GroupCreate");
                }
            else
            {
                return RedirectToAction("GroupCreate");
            }
        }
        [HttpGet]
        public ActionResult GroupEdit(int Id)
        {
            if (_adminProvider.UserAdmin(User.Identity.Name))
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
            else
            {
                return Error();
            }
            
        }
        [HttpPost]
        public ActionResult GroupEdit(GroupEditModel model)
        {
            if (ModelState.IsValid && _adminProvider.GroupEdit(model))
            {
                return GroupIndex();
            }
            else
            {
                return RedirectToAction("GroupEdit", model.Id);
            }
        }
        [HttpGet]
        public ActionResult GroupDelete(int Id)
        {
            if (_adminProvider.UserAdmin(User.Identity.Name))
            {
                Group group = _adminProvider.GroupLoad(Id);
                GroupDeleteModel delete = new GroupDeleteModel
                {
                    Name = group.Name
                };
                return View(delete);
            }
            else
            {
                return Error();
            }
            
        }
        [HttpPost]
        public ActionResult GroupDelete(GroupDeleteModel model)
        {
            _adminProvider.GroupDelete(model);
            return GroupIndex();
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
        public ActionResult Error()
        {
            return View();
        }
    }
}

