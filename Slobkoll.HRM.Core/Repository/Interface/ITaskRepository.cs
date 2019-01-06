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
    }
}
