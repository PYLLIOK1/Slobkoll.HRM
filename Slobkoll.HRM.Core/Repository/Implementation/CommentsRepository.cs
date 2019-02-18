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
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ISession _session;
        public CommentsRepository(ISession session)
        {
            _session = session;

        }
        public void AddComment(Comments model)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(model);
                transaction.Commit();
            }
        }
    }
}
