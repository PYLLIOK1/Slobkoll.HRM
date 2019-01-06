using NHibernate;
using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using System;


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
        public void TaskUpdate(Task task)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(task);
                transaction.Commit();
            }
        }
    }
}
