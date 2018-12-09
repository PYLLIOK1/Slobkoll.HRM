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
        private readonly IUserRepository _userRepository;
        public GroupRepository(ISession session, IUserRepository userRepository)
        {
            _session = session;
            _userRepository = userRepository;
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

        public void UserAddInGroup(int[] idUser, Group group)
        {
            List<User> list = _userRepository.ListUserAct().ToList();
            foreach (var item in idUser)
            {
                User user = list.First(x => x.Id == item);
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

        public Group CreateGroup(Group group)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(group);
                transaction.Commit();
            }
            return group;
        }

        public void DeleteGroup(Group group)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(group);
                transaction.Commit();
            }
        }

        public void ClearGroup(User user)
        {
            List<Group> list = user.Group.ToList();
            foreach (var item in list)
            {
                item.User.Remove(user);
                EditGroup(item);
            }
        }

        public Group LoadGroup(int id)
        {
            return _session.Load<Group>(id);
        }
    }
}
