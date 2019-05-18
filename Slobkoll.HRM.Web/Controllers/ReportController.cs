using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Slobkoll.HRM.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IHomeProvider _homeProvider;
        public ReportController(IHomeProvider homeProvider)
        {
            _homeProvider = homeProvider;
        }
        [Authorize]
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