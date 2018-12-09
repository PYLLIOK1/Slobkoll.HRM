using Slobkoll.ERP.Core.Object;
using System.Collections.Generic;

namespace Slobkoll.ERP.Core.Repository.Interface
{
    public interface IGroupRepository
    {
        void AddInGroup(int[] idGroup, User User);
        IList<Group> ListGroup();
        void CreateGroup(Group group);
        void EditGroup(Group group);
        void DeleteGroup(Group group);
        void ClearGroup(User user);
    }
}
