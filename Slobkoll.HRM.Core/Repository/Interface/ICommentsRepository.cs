using Slobkoll.HRM.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slobkoll.HRM.Core.Repository.Interface
{
    public interface ICommentsRepository
    {
        void AddComment(Comments model);
    }
}
