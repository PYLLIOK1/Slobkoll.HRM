using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slobkoll.ERP.Web.Providers.Interface
{
    public interface IAdminProvider
    {
        bool UserCreate (UserCreateModel model);
        IEnumerable<User> ListUser();
        IEnumerable<Group> ListGroup();
    }
}
