﻿using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Web.Models;
using System.Collections.Generic;

namespace Slobkoll.ERP.Web.Providers.Interface
{
    public interface IAdminProvider
    {
        bool UserCreate(UserCreateModel model);
        bool GroupCreate(GroupCreateModel model);
        IEnumerable<User> ListUser();
        IEnumerable<Group> ListGroup();
        IEnumerable<User> ListToUser();
        User UserLoad(int id);
        Group GroupLoad(int id);
        void UserEdit(UserEditModel model);
        void GroupEdit(GroupEditModel model);
        void GroupDelete(GroupDeleteModel model);
    }
}
