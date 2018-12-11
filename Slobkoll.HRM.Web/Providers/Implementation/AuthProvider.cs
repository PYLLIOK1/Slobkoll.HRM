using Slobkoll.HRM.Core.Repository.Interface;
using Slobkoll.HRM.Web.Models;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Slobkoll.HRM.Web.Providers.Implementation
{
    public class AuthProvider : IAuthProvider
    {
        private readonly IUserRepository _userRepository;
        public AuthProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public bool IsLoggedIn
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
        public bool Login(LoginModel model)
        {
            var user = _userRepository.Login(model.Login, model.Password);
            if (user == true)
            {
                FormsAuthentication.SetAuthCookie(model.Login, true);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

    }
}