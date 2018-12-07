﻿using Slobkoll.ERP.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slobkoll.ERP.Web.Providers.Interface
{
    public interface IAuthProvider
    {
        bool IsLoggedIn { get; }
        bool Login(LoginModel model);
        void Logout();
    }
}
