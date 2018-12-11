using Slobkoll.ERP.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slobkoll.ERP.Web.Providers.Interface
{
    public interface IHomeProvider
    {
        User UserLoginSerch(string login);
    }
}
