using NHibernate;
using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Slobkoll.ERP.Core.Repository.Implementation

{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;
        public UserRepository(ISession session)
        {
            _session = session;
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void EditUser(User user)
        {
            throw new NotImplementedException();
        }

        public IList<User> ListUserAct()
        {
            return _session.Query<User>().Where(x => x.StatusUser == true).ToList();
        }

        public IList<User> ListUserAll()
        {
            return _session.Query<User>().ToList();
        }

        public User LoadUser(int id)
        {
            return _session.Load<User>(id);
        }

        public User SelectUser(string Login)
        {
            return _session.Query<User>().First(x => x.Login == Login);
        }
    }
}
