using Slobkoll.HRM.Core.Object;
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
        public HomeProvider(IUserRepository userRepository, IGroupRepository groupRepository, ITaskRepository taskRepository, ISubTaskRepository subTaskRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
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
                TimeSpan date = item.DateEnd - DateTime.Now;
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
                TimeSpan date = item.DateEnd - DateTime.Now;
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
                TimeSpan date = item.DateEnd - DateTime.Now;
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
    }
}