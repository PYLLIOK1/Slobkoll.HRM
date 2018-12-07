using Slobkoll.ERP.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slobkoll.ERP.Core.Repository.Interface
{
    public interface IGroupRepository
    {
        IList<Group> AddInGroup(int[] idGroup, User User);
        IList<Group> ListGroup();
    }
}
