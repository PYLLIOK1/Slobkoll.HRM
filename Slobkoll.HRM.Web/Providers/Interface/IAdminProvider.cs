using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Web.Models;
using System;
using System.Collections.Generic;

namespace Slobkoll.HRM.Web.Providers.Interface
{
    public interface IAdminProvider
    {
        bool UserCreate(UserCreateModel model);
        bool GroupCreate(GroupCreateModel model);
        bool UserEdit(UserEditModel model);
        IEnumerable<User> ListUser();
        IEnumerable<Group> ListGroup();
        IEnumerable<User> ListToUser();
        User UserLoad(int id);
        Group GroupLoad(int id);
        bool GroupEdit(GroupEditModel model);
        void GroupDelete(GroupDeleteModel model);
        bool UserAdmin(string name);
        List<Task> ListTaskToDate(DateTime datetime1, DateTime datetime2);
    }
}
