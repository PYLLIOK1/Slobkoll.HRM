using NHibernate;
using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Core.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Slobkoll.ERP.Core.Repository.Implementation
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ISession _session;
        public GroupRepository(ISession session)
        {
            _session = session;
        }

        public void AddInGroup(int[] idGroup, User user)
        {
            List<Group> list = ListGroup().ToList();
            foreach (var item in idGroup)
            {
                Group group = list.First(x => x.Id == item);
                group.User.Add(user);
                EditGroup(group);
            }
        }
        public IList<Group> ListGroup()
        {
            return _session.Query<Group>().ToList();
        }
        public void EditGroup(Group group)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(group);
                transaction.Commit();
            }
        }
        public void CreateGroup(Group group)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(group);
                transaction.Commit();
            }
        }
    }
}
