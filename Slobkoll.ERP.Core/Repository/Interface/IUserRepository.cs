using Slobkoll.ERP.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slobkoll.ERP.Core.Repository.Interface
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void EditUser(User user);
        IList<User> ListUserAll();
        IList<User> ListUserAct();
        User LoadUser(int id);
        User SelectUser(string Login);

    }
}
