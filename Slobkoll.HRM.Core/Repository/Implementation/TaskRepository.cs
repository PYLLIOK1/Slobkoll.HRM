using NHibernate;
using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Slobkoll.HRM.Core.Repository.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ISession _session;
        public TaskRepository(ISession session)
        {
            _session = session;
        }


        public Task TaskCreate(Task task)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(task);
                transaction.Commit();
            }
            return task;
        }

        public Task TaskLoad(int id)
        {
            return _session.Load<Task>(id);
        }

        public IList<Task> LoadTaskAll()
        {
            return _session.Query<Task>().ToList();
        }

        public IList<Task> LoadTaskAllAct()
        {
            return _session.Query<Task>().Where(x => x.Change == true).ToList();
        }

        public void TaskUpdate(Task task)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(task);
                transaction.Commit();
            }
        }

        public IList<Task> TaskListAuthor(User user)
        {
            return _session.Query<Task>().Where( x => (x.Author == user && x.Change == true)).ToList();
        }
        public IList<Task> TaskListPerfomer(User user)
        {
            List<Task> result = new List<Task>();
            IList<Task> tasks = LoadTaskAll();
            foreach (Task item in tasks)
            {
                if(item.Change == true)
                {
                    foreach (var itemsub in item.SubTask)
                    {
                        if (itemsub.Performer == user)
                        {
                            result.Add(item);
                            continue;
                        }
                    }
                }
            }
            return result;
        }
        public IList<Task> TaskListObserver(User user)
        {
            List<Task> result = new List<Task>();
            foreach (var item in user.UserObserved)
            {
                result.AddRange(_session.Query<Task>().Where(x => (x.Author == item && x.Change == true)).ToList());
            }
            return result;
        }
        public IList<Task> TaskListAuthorArchive(User user)
        {
            return _session.Query<Task>().Where(x => (x.Author == user && x.Change == false)).ToList();
        }
        public IList<Task> TaskListObserverArchive(User user)
        {
            List<Task> result = new List<Task>();
            foreach (var item in user.UserObserved)
            {
                result.AddRange(_session.Query<Task>().Where(x => (x.Author == item && x.Change == false)).ToList());
            }
            return result;
        }
    }
}
