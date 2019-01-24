using Slobkoll.HRM.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Slobkoll.HRM.Core.Repository.Interface
{
    public interface ITaskRepository
    {
        Task TaskCreate(Task task);
        void TaskUpdate(Task task);
        Task TaskLoad(int id);
        IList<Task> LoadTaskAll();
        IList<Task> LoadTaskAllAct();
        IList<Task> TaskListAuthor(User user);
        IList<Task> TaskListPerfomer(User user);
        IList<Task> TaskListObserver(User user);
        IList<Task> TaskListAuthorArchive(User user);
        IList<Task> TaskListObserverArchive(User user);

    }
}
