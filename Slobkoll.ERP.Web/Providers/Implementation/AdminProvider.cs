using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Core.Repository.Interface;
using Slobkoll.ERP.Web.Models;
using Slobkoll.ERP.Web.Providers.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Slobkoll.ERP.Web.Providers.Implementation
{
    public class AdminProvider : IAdminProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        public AdminProvider(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public bool UserCreate(UserCreateModel model)
        {
            User user = null;
            user = _userRepository.ListUserAll().FirstOrDefault(x => x.Login == model.Login);
            if (user == null)
            {
                user = _userRepository.CreateUser(new User
                {
                    Login = model.Login,
                    Password = model.Password,
                    Name = model.Name,
                    Position = model.Position,
                    StatusUser = true,
                    AdminRole = false,
                });
                if (model.IdGroup != null)
                {
                    _groupRepository.AddInGroup(model.IdGroup, user);
                }
                if (model.UserIdObserver != null)
                {
                    _userRepository.UserAddObserver(model.UserIdObserver, user);
                }
                if (model.UserIdObserved != null)
                {
                    _userRepository.UserAddObserved(model.UserIdObserved, user);
                }
                if (model.UserIdCustomer != null)
                {
                    _userRepository.UserAddCustomer(model.UserIdCustomer, user);
                }
                if (model.UserIdPerfomers != null)
                {
                    _userRepository.UserAddPerformer(model.UserIdPerfomers, user);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<User> ListUser()
        {
            var list = _userRepository.ListUserAct();
            IEnumerable<User> listas = list as IEnumerable<User>;
            return listas;
        }
        public IEnumerable<User> ListToUser()
        {
            var list = _userRepository.ListUserAll();
            IEnumerable<User> listas = list as IEnumerable<User>;
            return listas;
        }
        public IEnumerable<Group> ListGroup()
        {
            var list = _groupRepository.ListGroup();
            IEnumerable<Group> listas = list as IEnumerable<Group>;
            return listas;
        }
        public User UserLoad(int id)
        {
            return _userRepository.LoadUser(id);
        }
        public void UserEdit(UserEditModel model)
        {
            User user = _userRepository.LoadUser(model.Id);
            user.Login = model.Login;
            user.Password = model.Password;
            user.Name = model.Name;
            user.Position = model.Position;
            user.AdminRole = model.AdminRole;
            user.StatusUser = model.StatusUser;
            if (model.IdGroup != null)
            {
                _groupRepository.ClearGroup(user);
                _groupRepository.AddInGroup(model.IdGroup, user);
            }
            if (model.UserIdObserver != null)
            {
                user.UserObserver.Clear();
                _userRepository.UserAddObserver(model.UserIdObserver, user);
            }
            if (model.UserIdObserved != null)
            {
                user.UserObserved.Clear();
                _userRepository.UserAddObserved(model.UserIdObserved, user);
            }
            if (model.UserIdCustomer != null)
            {
                user.UserCustomer.Clear();
                _userRepository.UserAddCustomer(model.UserIdCustomer, user);
            }
            if (model.UserIdPerfomer != null)
            {
                user.UserPerformer.Clear();
                _userRepository.UserAddPerformer(model.UserIdPerfomer, user);
            }
            _userRepository.EditUser(user);
        }


        public bool GroupCreate(GroupCreateModel model)
        {
            Group group = null;
            group = _groupRepository.ListGroup().FirstOrDefault(x => x.Name == model.Name);
            if (group == null)
            {
                group = _groupRepository.CreateGroup(new Group
                {
                    Name = model.Name
                });
                if (model.GroupUser != null)
                {
                    _groupRepository.UserAddInGroup(model.GroupUser, group);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public Group GroupLoad(int id)
        {
            return _groupRepository.LoadGroup(id);
        }
        public void GroupEdit(GroupEditModel model)
        {
            Group group = _groupRepository.LoadGroup(model.Id);
            group.Name = model.Name;
            group.User.Clear();
            _groupRepository.EditGroup(group);
            if (model.GroupUser != null)
            {
                _groupRepository.UserAddInGroup(model.GroupUser, group);
            }
        }
        public void GroupDelete(GroupDeleteModel model)
        {
            Group group = GroupLoad(model.Id);
            _groupRepository.DeleteGroup(group);
        }
    }
}