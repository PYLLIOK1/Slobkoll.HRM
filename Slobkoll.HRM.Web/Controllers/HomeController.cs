using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            ViewBag.Id = user.Id;
            ViewBag.UserHome = user;
            return View();
        }
        public PartialViewResult ListAuthor(int id)
        {
            var model = _homeProvider.TaskListAuthor(id);
            return PartialView(model.Reverse());
        }
        public PartialViewResult ListPerfomer(int id)
        {
            var model = _homeProvider.TaskListPerfomer(id);
            return PartialView(model.Reverse());
        }
        public PartialViewResult ListObserver(int id)
        {
            var model = _homeProvider.TaskListObserver(id);
            return PartialView(model.Reverse());
        }
        public PartialViewResult ListArchive(int id)
        {
            var model = _homeProvider.TaskListAuthorArchive(id);
            return PartialView(model.Reverse());
        }
        public PartialViewResult ListObserverArchive(int id)
        {
            var model = _homeProvider.TaskListObserverArchive(id);
            return PartialView(model.Reverse());
        }


        public ActionResult TaskAuthor(int id)
        {
            var model = _homeProvider.TaskLoad(id);
            model = _homeProvider.CheckAuthor(model);
            return PartialView(model);
        }
        public ActionResult TaskPerfomer(int id)
        {
            var task = _homeProvider.TaskLoad(id);
            var model = task.SubTask.FirstOrDefault(x => x.Performer.Login == User.Identity.Name);
            model = _homeProvider.CheckPerfomer(model);
            return PartialView(model);
        }
        public ActionResult TaskObserver(int id)
        {
            var model = _homeProvider.TaskLoad(id);
            return PartialView(model);
        }
        public ActionResult TaskArchive(int id)
        {
            var model = _homeProvider.TaskLoad(id);
            return PartialView(model);
        }
        public ActionResult TaskArchiveObserver(int id)
        {
            var model = _homeProvider.TaskLoad(id);
            return PartialView(model);
        }


        [Authorize]
        [HttpGet]
        public ActionResult AddTask()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.Id = user.Id;
            ViewBag.UserHome = user;
            ViewBag.User = new SelectList(_homeProvider.SelectPerfomer(user.Id), "Id", "Name");
            ViewBag.Group = new SelectList(_homeProvider.SelecGroupPerfomer(user.Id), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult AddTask(TaskCreateModel model)
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.Id = user.Id;
            ViewBag.UserHome = user;
            ViewBag.User = new SelectList(_homeProvider.SelectPerfomer(user.Id), "Id", "Name", model.UserIdPerfomers);
            ViewBag.Group = new SelectList(_homeProvider.SelecGroupPerfomer(user.Id), "Id", "Name", model.UserIdPerfomerGroup);
            if (ModelState.IsValid)
            {
                if (model.UserIdPerfomerGroup != null || model.UserIdPerfomers != null)
                {
                    if (model.File != null)
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
        [Authorize]
        [HttpGet]
        public ActionResult EditTask(int id)
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.Id = user.Id;
            ViewBag.UserHome = user;
            var task = _homeProvider.LoadEditTask(id);
            var userAuthor = _homeProvider.TaskLoad(id).Author;
            if (user == userAuthor)
            {
                return View(task);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult EditTask(TaskEdit model)
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            if (ModelState.IsValid)
            {
                _homeProvider.TaskEdit(model);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Id = user.Id;
                ViewBag.UserHome = user;
                return View(model);
            }
        }

        [HttpPost]
        public int AddPerfomerFile(SubTaskModelEdit model)
        {
            var subtask = _homeProvider.SubTaskLoad(model.Id);
            if (model.File != null)
            {
                _homeProvider.SubTaskEdit(model.Id, model.File, Path.GetFileName(model.File.FileName));
            }

            return subtask.TaskId.Id;
        }

        public string TaskFileDownload(int id)
        {
            var Task = _homeProvider.TaskLoad(id);
            return Task.Files;
        }
        public string SubTaskFileDownload(int id)
        {
            var SubTask = _homeProvider.SubTaskLoad(id);
            return SubTask.Files;
        }

        [HttpPost]
        public int EditStatusPerfomer(int id, string text)
        {
            var subtask = _homeProvider.SubTaskLoad(id);
            _homeProvider.SubTaskStatusEdit(id, text);
            return subtask.TaskId.Id;
        }

        public int AddCommentAuthor(int idSubTask, string commentText, int idTask)
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            _homeProvider.AddCommentAuthor(user, idSubTask, commentText);
            return idTask;
        }
        public int AddCommentPerfomer(int idSubTask, string commentText, int idTask)
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            _homeProvider.AddCommentPerfomer(user, idSubTask, commentText);
            return idTask;
        }

        public ActionResult Reports()
        {
            var user = _homeProvider.UserLoginSerch(User.Identity.Name);
            ViewBag.Id = user.Id;
            ViewBag.UserHome = user;
            return View();
        }
        public ActionResult ReportsSearch(string date1, string date2)
        {
            date2 += " 23:59:59";
            if (DateTime.TryParse(date1, out DateTime dateTime1) && DateTime.TryParse(date2, out DateTime dateTime2))
            {
                List<Task> result = _homeProvider.ListTaskToDate(dateTime1, dateTime2);
                ViewBag.date1 = dateTime1.ToString();
                ViewBag.date2 = dateTime2.ToString();
                ViewBag.res = result.Count();
                ViewBag.green = result.Where(x => x.Status == "Выполнено").Count();
                ViewBag.yellow = result.Where(x => x.Status == "Выполняется").Count();
                result = result.Where(x => x.Status == "Не выполнено").ToList();
                ViewBag.red = result.Count();
                List<SubTask> subTasks = new List<SubTask>();
                foreach (var item in result)
                {
                    foreach (var item1 in item.SubTask)
                    {
                        if (item1.Status != "Выполнено")
                        {
                            subTasks.Add(item1);
                        }
                    }
                }
                return PartialView(subTasks);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Введите дату');</script>");
            }

        }

    }
}