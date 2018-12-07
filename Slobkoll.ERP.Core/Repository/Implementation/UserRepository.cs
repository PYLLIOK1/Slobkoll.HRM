using NHibernate;
using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Core.Repository.Interface;
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

        public User CreateUser(User user)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(user);
                transaction.Commit();
            }
            return LoadUser(user.Id);
        }


        public void EditUser(User user)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(user);
                transaction.Commit();
            }
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

        public bool Login(string login, string password)
        {
            var user = ListUserAct().First(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User SelectUser(string Login)
        {
            return _session.Query<User>().First(x => x.Login == Login);
        }

        public void UserAddObserver(int[] observer, User user)
        {
            List<User> ListUser = ListUserAct().ToList();
            foreach (var item in observer)
            {
                User User = ListUser.First(x => x.Id == item);
                User.UserObserved.Add(user);
                EditUser(User);
            }
        }

        public void UserAddObserved(int[] observed, User user)
        {
            List<User> ListUser = ListUserAct().ToList();
            foreach (var item in observed)
            {
                User User = ListUser.First(x => x.Id == item);
                User.UserObserver.Add(user);
                EditUser(User);
            }
        }

        public void UserAddCustomer(int[] customer, User user)
        {
            List<User> ListUser = ListUserAct().ToList();
            foreach (var item in customer)
            {
                User User = ListUser.First(x => x.Id == item);
                User.UserPerformer.Add(user);
                EditUser(User);
            }
        }

        public void UserAddPerformer(int[] perfomer, User user)
        {
            List<User> ListUser = ListUserAct().ToList();
            foreach (var item in perfomer)
            {
                User User = ListUser.First(x => x.Id == item);
                User.UserCustomer.Add(user);
                EditUser(User);
            }
        }
    }
}
