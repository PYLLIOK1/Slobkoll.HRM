using Slobkoll.HRM.Core.Object;
using System.Collections.Generic;

namespace Slobkoll.HRM.Core.Repository.Interface
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        void UserAddObserver(int[] observer, User user);
        void UserAddObserved(int[] observed, User user);
        void UserAddCustomer(int[] customer, User user);
        void UserAddPerformer(int[] perfomer, User user);
        void EditUser(User user);
        IList<User> ListUserAll();
        IList<User> ListUserAct();
        User LoadUser(int id);
        User SelectUser(string Login);
        bool Login(string name, string password);
        IList<User> ListUserPerfomerSelect(int id);
        IList<User> UserSelectSubtask(int[] id);
    }
}
