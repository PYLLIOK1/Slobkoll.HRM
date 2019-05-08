using NHibernate;
using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using System;

namespace Slobkoll.HRM.Core.Repository.Implementation
{
    public class SubTaskModRepository : ISubTaskModRepository
    {
        private readonly ISession _session;
        public SubTaskModRepository(ISession session)
        {
            _session = session;

        }
        public void AddSubTaskMod(SubTaskMod mod)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(mod);
                transaction.Commit();
            }
        }
    }
}
