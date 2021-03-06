﻿using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Slobkoll.HRM.Web.Providers.Implementation
{
    public class HomeProvider : IHomeProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly ICommentsRepository _commentRepository;
        private readonly ISubTaskModRepository _subTaskModRepository;
        public HomeProvider(IUserRepository userRepository, IGroupRepository groupRepository, ITaskRepository taskRepository, ISubTaskRepository subTaskRepository, ICommentsRepository commentRepository, ISubTaskModRepository subTaskModRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
            _commentRepository = commentRepository;
            _subTaskModRepository = subTaskModRepository;
        }
        public Task TaskLoad(int id)
        {
            return _taskRepository.TaskLoad(id);
        }

        public SubTask SubTaskLoad(int id)
        {
            return _subTaskRepository.SubTaskLoad(id);
        }

        public IEnumerable<User> SelectPerfomer(int id)
        {
            return _userRepository.ListUserPerfomerSelect(id) as IEnumerable<User>;
        }

        public User UserLoginSerch(string login)
        {
            return _userRepository.SelectUser(login);
        }

        public void TaskCreate(TaskCreateModel model, User user)
        {
            Task task = new Task
            {
                Name = model.Name,
                Description = model.Description,
                DateBegin = DateTime.Now,
                DateEnd = model.DateTime,
                FileName = Path.GetFileName(model.File.FileName),
                Change = true,
                Status = "Выполняется",
                Author = user
            };
            task = _taskRepository.TaskCreate(task);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("Slob/TaskFiles/" + task.Id));
            task.Files = "Slob/TaskFiles/" + task.Id + "/" + task.FileName;
            model.File.SaveAs(HttpContext.Current.Server.MapPath("Slob/TaskFiles/" + task.Id + "/" + task.FileName));
            SubTasksCreate(model.UserIdPerfomers, model.UserIdPerfomerGroup, task);
        }

        public void SubTasksCreate(int[] user, int[] group, Task task)
        {
            List<User> list = null;
            if (user != null)
            {
                IList<User> users = _userRepository.UserSelectSubtask(user).ToList();
                foreach (var item in users)
                {
                    if (list != null)
                    {
                        list.Add(item);
                    }
                    else
                    {
                        list = new List<User> { item };
                    }
                }
            }
            if (group != null)
            {
                IList<User> groupuser = _groupRepository.ListUserGroup(group).ToList();
                foreach (var item in groupuser)
                {
                    if(item.StatusUser == true)
                    {
                        if (list != null)
                        {
                            list.Add(item);
                        }
                        else
                        {
                            list = new List<User> { item };
                        }
                    }
                    
                }
            }
            list.Distinct();
            foreach (var item in list)
            {
                SubTask subTask = new SubTask
                {
                    ChangePerformer = true,
                    Performer = item,
                    TaskId = task,
                    Status = "Ожидает проверки исполнителем",
                    ChangeAuthor = false,
                };
                _subTaskRepository.SubTaskCreate(subTask);
                string message = "У вас новая задача";
                SendMessage(subTask.Performer.Login, message, subTask.TaskId.Id, "TaskPerfomer");
            }
        }

        public TaskEdit LoadEditTask(int id)
        {
            var model = _taskRepository.TaskLoad(id);
            TaskEdit task = new TaskEdit
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                DateTime = model.DateEnd,
            };
            return task;
        }

        public void TaskEdit(TaskEdit model)
        {
            var task = _taskRepository.TaskLoad(model.Id);
            task.Name = model.Name;
            task.Description = model.Description;
            task.DateEnd = model.DateTime;
            if (model.File != null)
            {
                task.FileName = Path.GetFileName(model.File.FileName);
                task.Files = "/TaskFiles/" + task.Id + "/" + task.FileName;
            }

            _taskRepository.TaskUpdate(task);
            foreach (var item in task.SubTask)
            {
                SubTaskMod mod = new SubTaskMod
                {
                    SubTaskId = item,
                    Status = "Задача изменена",
                    DateTime = DateTime.Now
                };
                _subTaskModRepository.AddSubTaskMod(mod);
                string message = "Задача №" + item.TaskId.Id + " изменена";
                SendMessage(item.Performer.Login, message, item.TaskId.Id, "TaskPerfomer");
            }
        }

        public IEnumerable<Group> SelecGroupPerfomer(int id)
        {
            return _groupRepository.ListGroupPerfomerSelect(id) as IEnumerable<Group>;
        }

        public void SubTaskEdit(int id, HttpPostedFileBase file, string name)
        {
            var model = _subTaskRepository.SubTaskLoad(id);
            model.ChangeAuthor = true;
            model.Status = "Ожидает проверки автора";
            model.Files = "Slob/SubTask/" + model.Id + "/" + name;
            model.FileName = name;
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("Slob/Subtask/" + model.Id + "/"));
            file.SaveAs(HttpContext.Current.Server.MapPath("Slob/Subtask/" + model.Id + "/" + name));
            _subTaskRepository.SubTaskEdit(model);
            SubTaskMod mod = new SubTaskMod
            {
                SubTaskId = model,
                Status = model.Status,
                DateTime = DateTime.Now
            };
            _subTaskModRepository.AddSubTaskMod(mod);
            string message = "Задача №" + model.TaskId.Id + " ждет вашей проверки";
            SendMessage(model.TaskId.Author.Login, message, model.TaskId.Id, "TaskAuthor");
        }

        public void SubTaskStatusEdit(int Id, string status)
        {
            var model = _subTaskRepository.SubTaskLoad(Id);
            model.Status = status;
            model.ChangePerformer = true;
            _subTaskRepository.SubTaskEdit(model);
            SubTaskMod mod = new SubTaskMod
            {
                SubTaskId = model,
                Status = status,
                DateTime = DateTime.Now
            };
            _subTaskModRepository.AddSubTaskMod(mod);
            string message = "Автор изменил статус задачи №" + model.TaskId.Id;
            SendMessage(model.Performer.Login, message, model.TaskId.Id, "TaskPerfomer");
        }

        public Task CheckAuthor(Task task)
        {
            foreach (var item in task.SubTask)
            {
                if (item.ChangeAuthor == true)
                {
                    item.ChangeAuthor = false;
                    if (item.Status == "Ожидает проверки автора")
                    {
                        item.Status = "Ожидает действий автора";
                        SubTaskMod mod = new SubTaskMod
                        {
                            SubTaskId = item,
                            Status = item.Status,
                            DateTime = DateTime.Now
                        };
                        _subTaskModRepository.AddSubTaskMod(mod);
                        string message = "Автор проверил задачи №" + item.TaskId.Id;
                        SendMessage(item.Performer.Login, message, item.TaskId.Id, "TaskPerfomer");
                    }
                    _subTaskRepository.SubTaskEdit(item);
                }
            }
            return task;
        }

        public SubTask CheckPerfomer(SubTask subTask)
        {
            if (subTask.ChangePerformer == true)
            {
                subTask.ChangePerformer = false;
                if (subTask.Status == "Ожидает проверки исполнителем")
                {
                    subTask.Status = "Ожидает действий исполнителем";
                    SubTaskMod mod = new SubTaskMod
                    {
                        SubTaskId = subTask,
                        Status = subTask.Status,
                        DateTime = DateTime.Now
                    };
                    _subTaskModRepository.AddSubTaskMod(mod);
                    string message = subTask.Performer.Name + " проверил задачи №" + subTask.TaskId.Id;
                    SendMessage(subTask.TaskId.Author.Login, message, subTask.TaskId.Id, "TaskAuthor");
                }
                _subTaskRepository.SubTaskEdit(subTask);
            }
            return subTask;
        }



        public IList<TaskListAuthor> TaskListAuthor(int id)
        {
            User user = _userRepository.LoadUser(id);
            var task = _taskRepository.TaskListAuthor(user);
            List<TaskListAuthor> taskLists = new List<TaskListAuthor>();
            foreach (var item in task)
            {
                bool infoauthor = false;
                foreach (var iteminfo in item.SubTask)
                {
                    if (iteminfo.ChangeAuthor)
                    {
                        infoauthor = true;
                    }
                }
                TimeSpan date = new TimeSpan(0, 0, 0);
                if (item.DateEnd > DateTime.Now)
                {
                    date = item.DateEnd - DateTime.Now;
                }
                TaskListAuthor taske = new TaskListAuthor()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status,
                    Username = item.Author.Name,
                    Time = date.Days.ToString() + " Д " + date.Hours.ToString() + " Ч " + date.Minutes.ToString() + " М",
                    Info = infoauthor
                };
                taskLists.Add(taske);
            }
            return taskLists;
        }

        public IList<TaskListAuthor> TaskListPerfomer(int id)
        {
            User user = _userRepository.LoadUser(id);
            var task = _taskRepository.TaskListPerfomer(user);
            List<TaskListAuthor> taskLists = new List<TaskListAuthor>();
            foreach (var item in task)
            {

                SubTask sub = item.SubTask.FirstOrDefault(x => x.Performer == user);
                TimeSpan date = new TimeSpan(0, 0, 0);
                if (item.DateEnd > DateTime.Now)
                {
                    date = item.DateEnd - DateTime.Now;

                }
                TaskListAuthor taske = new TaskListAuthor()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = sub.Status,
                    Username = item.Author.Name,
                    Time = date.Days.ToString() + " Д " + date.Hours.ToString() + " Ч " + date.Minutes.ToString() + " М",
                    Info = sub.ChangePerformer
                };
                taskLists.Add(taske);
            }
            return taskLists;
        }

        public IList<TaskListAuthor> TaskListObserver(int id)
        {
            User user = _userRepository.LoadUser(id);
            var task = _taskRepository.TaskListObserver(user);
            List<TaskListAuthor> taskLists = new List<TaskListAuthor>();
            foreach (var item in task)
            {
                bool infoauthor = false;
                foreach (var iteminfo in item.SubTask)
                {
                    if (iteminfo.ChangeAuthor)
                    {
                        infoauthor = true;
                    }
                }
                TimeSpan date = new TimeSpan(0, 0, 0);
                if (item.DateEnd > DateTime.Now)
                {
                    date = item.DateEnd - DateTime.Now;
                }
                TaskListAuthor taske = new TaskListAuthor()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Status = item.Status,
                    Username = item.Author.Name,
                    Time = date.Days.ToString() + " Д " + date.Hours.ToString() + " Ч " + date.Minutes.ToString() + " М",
                    Info = infoauthor
                };
                taskLists.Add(taske);
            }
            return taskLists;
        }

        public IList<TaskListArchive> TaskListAuthorArchive(int id)
        {
            User user = _userRepository.LoadUser(id);
            var task = _taskRepository.TaskListAuthorArchive(user);
            List<TaskListArchive> taskLists = new List<TaskListArchive>();
            foreach (var item in task)
            {
                TaskListArchive taske = new TaskListArchive()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Username = item.Author.Name,
                    Status = item.Status
                };
                taskLists.Add(taske);
            }
            return taskLists;
        }

        public IList<TaskListArchive> TaskListObserverArchive(int id)
        {
            User user = _userRepository.LoadUser(id);
            var task = _taskRepository.TaskListObserverArchive(user);
            List<TaskListArchive> taskLists = new List<TaskListArchive>();
            foreach (var item in task)
            {
                TaskListArchive taske = new TaskListArchive()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Username = item.Author.Name,
                    Status = item.Status
                };
                taskLists.Add(taske);
            }
            return taskLists;
        }



        public void AddCommentAuthor(User Author, int idSubTask, string CommentText)
        {
            var SubTask = _subTaskRepository.SubTaskLoad(idSubTask);
            SubTask.ChangePerformer = true;
            _subTaskRepository.SubTaskEdit(SubTask);
            Comments comment = new Comments
            {
                TextComment = CommentText,
                DateTime = DateTime.Now,
                Author = Author,
                SubTask = SubTask
            };
            _commentRepository.AddComment(comment);
            string message = "Автор добавил комментарий к задачи №" + SubTask.TaskId.Id;
            SendMessage(SubTask.Performer.Login, message, SubTask.TaskId.Id, "TaskPerfomer");
        }
        public void AddCommentPerfomer(User Author, int idSubTask, string CommentText)
        {
            var SubTask = _subTaskRepository.SubTaskLoad(idSubTask);
            SubTask.ChangeAuthor = true;
            _subTaskRepository.SubTaskEdit(SubTask);
            Comments comment = new Comments
            {
                TextComment = CommentText,
                DateTime = DateTime.Now,
                Author = Author,
                SubTask = SubTask
            };
            _commentRepository.AddComment(comment);
            string message = SubTask.Performer.Name + " добавил комментарий к задачи №" + SubTask.TaskId.Id;
            SendMessage(SubTask.TaskId.Author.Login, message, SubTask.TaskId.Id, "TaskAuthor");
        }


        private void SendMessage(string name, string text, int id, string view)
        {
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<MyHub>();
            foreach (var connectionId in MyHub._connections.GetConnections(name))
            {
                context.Clients.Client(connectionId).Message(text, id, view);
            }
        }
        public List<Task> ListTaskToDate(DateTime datetime1, DateTime datetime2)
        {
            List<Task> tasks = _taskRepository.LoadTaskAll().Where(X => X.DateBegin >= datetime1 && X.DateBegin <= datetime2).ToList();
            return tasks;
        }
    }
}