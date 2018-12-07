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

        public IList<Group> AddInGroup(int[] idGroup, User user)
        {
            List<Group> list = new List<Group>();
            foreach (var item in idGroup)
            {
                Group group = ListGroup().First(x => x.Id == item);
                group.User.Add(user);
                list.Add(group);
            }
            return list;
        }
        public IList<Group> ListGroup()
        {
            return _session.Query<Group>().ToList();
        }
    }
}
