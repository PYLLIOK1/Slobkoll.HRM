using Slobkoll.HRM.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slobkoll.HRM.Web.Providers.Interface
{
    public interface IHomeProvider
    {
        User UserLoginSerch(string login);
    }
}
