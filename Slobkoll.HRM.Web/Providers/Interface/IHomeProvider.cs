using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Web.Models;
using System.Collections.Generic;


namespace Slobkoll.HRM.Web.Providers.Interface
{
    public interface IHomeProvider
    {
        User UserLoginSerch(string login);
        IEnumerable<User> SelectPerfomer(int id);
        IEnumerable<Group> SelecGroupPerfomer(int id);
        void TaskCreate(TaskCreateModel model, User user);
        void SubTasksCreate(int[] user, int[] group, Task task);
    }
}
