using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Core.Repository.Interface;
using Slobkoll.ERP.Web.Models;
using Slobkoll.ERP.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                if(model.IdGroup != null)
                {
                    var list = _groupRepository.AddInGroup(model.IdGroup, user);
                    foreach (var item in list)
                    {
                        user.Group.Add(item);
                    }
                    _userRepository.Usersave(user);
                }
                if(model.UserIdObserver != null)
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
        public IEnumerable<Group> ListGroup()
        {
            var list = _groupRepository.ListGroup();
            IEnumerable<Group> listas = list as IEnumerable<Group>;
            return listas;
        }
    }
}