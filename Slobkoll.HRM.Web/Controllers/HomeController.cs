using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;
using System.IO;
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
            ViewBag.Id = user.Id;
            return View();
        }
        public PartialViewResult ListAuthor(int id)
        {
            var model = _homeProvider.TaskListAuthor(id);
            return PartialView(model);
        }
        public PartialViewResult ListPerfomer(int id)
        {
            var model = _homeProvider.TaskListPerfomer(id);
            return PartialView(model);
        }
        public PartialViewResult ListObserver(int id)
        {
            var model = _homeProvider.TaskListObserver(id);
            return PartialView(model);
        }
        public PartialViewResult ListArchive(int id)
        {
            var model = _homeProvider.TaskListAuthorArchive(id);
            return PartialView(model);
        }
        public PartialViewResult ListObserverArchive(int id)
        {
            var model = _homeProvider.TaskListObserverArchive(id);
            return PartialView(model);
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
            var task = _homeProvider.LoadEditTask(id);
            if (user == task.Author)
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
            if (ModelState.IsValid)
            {
                _homeProvider.TaskEdit(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public int AddPerfomerFile(SubTaskModelEdit model)
        {
            var subtask = _homeProvider.SubTaskLoad(model.Id);
            if (model.File != null)
            {
                byte[] File = null;
                using (var binaryReader = new BinaryReader(model.File.InputStream))
                {
                    File = binaryReader.ReadBytes(model.File.ContentLength);
                }
                _homeProvider.SubTaskEdit(model.Id, File, Path.GetFileName(model.File.FileName));
            }
            return subtask.TaskId.Id;
        }




        public string TaskFileDownload(int id)
        {
            var Task = _homeProvider.TaskLoad(id);
            byte[] file = Task.Files;
            Directory.CreateDirectory(Server.MapPath("/Temp/"));
            string path = Server.MapPath("/Temp/" + Task.FileName);
            using (FileStream fstream = new FileStream(path, FileMode.Create))
            {
                fstream.Write(file, 0, file.Length);
            }
            return Task.FileName;
        }
        public string SubTaskFileDownload(int id)
        {
            var SubTask = _homeProvider.SubTaskLoad(id);
            byte[] file = SubTask.Files;
            Directory.CreateDirectory(Server.MapPath("/Temp/"));
            string path = Server.MapPath("/Temp/" + SubTask.Name);
            using (FileStream fstream = new FileStream(path, FileMode.Create))
            {
                fstream.Write(file, 0, file.Length);
            }
            return SubTask.Name;
        }

        [HttpPost]
        public int EditStatusPerfomer(int id, string text)
        {
            var subtask = _homeProvider.SubTaskLoad(id);
            _homeProvider.SubTaskStatusEdit(id, text);
            return subtask.TaskId.Id;
        }
    }
}