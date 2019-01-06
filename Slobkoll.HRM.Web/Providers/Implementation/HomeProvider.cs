using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;

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
            SubTasksCreate(model.UserIdPerfomers,model.UserIdPerfomerGroup, task);
        }

        public void SubTasksCreate (int[] user, int[] group, Task task)
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


        public IEnumerable<Group> SelecGroupPerfomer(int id)
        {
            return _groupRepository.ListGroupPerfomerSelect(id) as IEnumerable<Group>;
        }
    }
}