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
        TaskEdit LoadEditTask(int id);
        void TaskEdit(TaskEdit model);
        IList<TaskListAuthor> TaskListAuthor(int id);
        IList<TaskListAuthor> TaskListPerfomer(int id);
        IList<TaskListAuthor> TaskListObserver(int id);
        IList<TaskListArchive> TaskListAuthorArchive(int id);
        IList<TaskListArchive> TaskListObserverArchive(int id);

    }
}
