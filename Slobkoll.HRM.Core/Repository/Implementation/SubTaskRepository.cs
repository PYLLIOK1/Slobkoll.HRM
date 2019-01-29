using NHibernate;
using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slobkoll.HRM.Core.Repository.Implementation
{
    public class SubTaskRepository : ISubTaskRepository
    {
        private readonly ISession _session;
        public SubTaskRepository(ISession session)
        {
            _session = session;
        }
        public void SubTaskCreate(SubTask subTask)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(subTask);
                transaction.Commit();
            }
        }
        public void SubTaskEdit(SubTask subTask)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(subTask);
                transaction.Commit();
            }
        }
        public SubTask SubTaskLoad(int id)
        {
            return _session.Load<SubTask>(id);
        }
    }
}
