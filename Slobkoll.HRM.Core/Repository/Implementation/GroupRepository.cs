using NHibernate;
using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Slobkoll.HRM.Core.Repository.Implementation
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

        public void AddInGroupPerfomer(int[] GroupPerfomer, User user)
        {
            List<Group> list = ListGroup().ToList();
            foreach (var item in GroupPerfomer)
            {
                Group group = list.First(x => x.Id == item);
                group.UserPerformer.Add(user);
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

        public int[] GroupIdInList(int[] idGrouplist)
        {
            List<Group> list = ListGroup().ToList();
            IList<int> listint = new List<int>();
            foreach (var item in idGrouplist)
            {
                Group group = list.First(x => x.Id == item);
                listint.Add(item);
            }
            return listint.ToArray();
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

        public void PerfomerClearGroup(User user)
        {
            List<Group> list = user.GroupPerformer.ToList();
            foreach (var item in list)
            {
                item.UserPerformer.Remove(user);
                EditGroup(item);
            }
        }

        public Group LoadGroup(int id)
        {
            return _session.Load<Group>(id);
        }

        public IList<Group> ListGroupPerfomerSelect(int id)
        {
            return _userRepository.LoadUser(id).GroupPerformer.ToList();
        }
        public IList<User> ListUserGroup(int[] idGroups)
        {
            List<User> users = null;
            foreach (var item in idGroups)
            {
                Group group = LoadGroup(item);
                if(users == null)
                {
                    users = new List<User>(group.User);
                }
                else
                {
                    users.Concat(group.User);
                }  
            }
            return users;
        }
    }
}
