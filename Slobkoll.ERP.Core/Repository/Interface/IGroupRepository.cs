using Slobkoll.ERP.Core.Object;
using System.Collections.Generic;

namespace Slobkoll.ERP.Core.Repository.Interface
{
    public interface IGroupRepository
    {
        void AddInGroup(int[] idGroup, User User);
        void UserAddInGroup(int[] idGroup, Group group);
        IList<Group> ListGroup();
        Group CreateGroup(Group group);
        void EditGroup(Group group);
        void DeleteGroup(Group group);
        void ClearGroup(User user);
        void PerfomerClearGroup(User user);
        Group LoadGroup(int id);
        int[] GroupIdInList(int[] idGrouplist);
        void AddInGroupPerfomer(int[] GroupPerfomer, User user);
    }
}
