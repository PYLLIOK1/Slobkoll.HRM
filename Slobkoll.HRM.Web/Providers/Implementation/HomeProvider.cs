﻿using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Slobkoll.HRM.Web.Providers.Implementation
{
    public class HomeProvider : IHomeProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly ICommentsRepository _commentRepository;
        public HomeProvider(IUserRepository userRepository, IGroupRepository groupRepository, ITaskRepository taskRepository, ISubTaskRepository subTaskRepository, ICommentsRepository commentRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
            _commentRepository = commentRepository;
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
            byte[] file = null;
            using (var binaryReader = new BinaryReader(model.File.InputStream))
            {
                file = binaryReader.ReadBytes(model.File.ContentLength);
            }

            Task task = new Task
            {
                Name = model.Name,
                Description = model.Description,
                DateBegin = DateTime.Now,
                DateEnd = model.DateTime,
                FileName = Path.GetFileName(model.File.FileName),
                Files = file,
                Change = true,
                Status = "Выполняется",
                Author = user
            };
            task = _taskRepository.TaskCreate(task);
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
            list.Distinct();
            foreach (var item in list)
            {
                SubTask subTask = new SubTask
                {
                    ChangePerformer = true,
                    Performer = item,
                    TaskId = task,
                    Status = "Ожидает проверки исполнителем",
                    ChangeAuthor = false
                };
                _subTaskRepository.SubTaskCreate(subTask);
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
                Author = model.Author

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
                byte[] file = null;
                using (var binaryReader = new BinaryReader(model.File.InputStream))
                {
                    file = binaryReader.ReadBytes(model.File.ContentLength);
                }
                task.Files = file;
            }
            _taskRepository.TaskUpdate(task);
        }

        public IEnumerable<Group> SelecGroupPerfomer(int id)
        {
            return _groupRepository.ListGroupPerfomerSelect(id) as IEnumerable<Group>;
        }

        public void SubTaskEdit(int id, byte[] file, string name)
        {
            var model = _subTaskRepository.SubTaskLoad(id);
            model.ChangeAuthor = true;
            model.Status = "Ожидает проверки автора";
            model.Files = file;
            model.Name = name;
            _subTaskRepository.SubTaskEdit(model);

        }

        public void SubTaskStatusEdit(int Id,string status)
        {
            var model = _subTaskRepository.SubTaskLoad(Id);
            model.Status = status;
            model.ChangePerformer = true;
            _subTaskRepository.SubTaskEdit(model);
        }

        public Task CheckAuthor(Task task)
        {
            foreach (var item in task.SubTask)
            {
                if(item.ChangeAuthor == true)
                {
                    item.ChangeAuthor = false;
                    if(item.Status == "Ожидает проверки автора")
                    {
                        item.Status = "Ожидает действий автора";
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
                    Time = date.ToString(),
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
                    Time = date.ToString(),
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
                    Time = date.ToString(),
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

        public void AddComment(User Author, int idSubTask, string CommentText )
        {
            Comments comment = new Comments
            {
                TextComment = CommentText,
                DateTime = DateTime.Now,
                Author = Author,
                SubTask = _subTaskRepository.SubTaskLoad(idSubTask)
            };
            _commentRepository.AddComment(comment);
        }
    }
}