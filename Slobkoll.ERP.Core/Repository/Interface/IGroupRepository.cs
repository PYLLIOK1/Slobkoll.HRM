﻿using Slobkoll.ERP.Core.Object;
using System.Collections.Generic;

namespace Slobkoll.ERP.Core.Repository.Interface
{
    public interface IGroupRepository
    {
        void AddInGroup(int[] idGroup, User User);
        IList<Group> ListGroup();
        void CreateGroup(Group group);
    }
}
