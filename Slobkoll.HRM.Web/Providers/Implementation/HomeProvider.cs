using Slobkoll.HRM.Core.Object;
using Slobkoll.HRM.Core.Repository.Interface;
using Slobkoll.HRM.Web.Providers.Interface;

namespace Slobkoll.HRM.Web.Providers.Implementation
{
    public class HomeProvider : IHomeProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        public HomeProvider(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public User UserLoginSerch(string login)
        {
            return _userRepository.SelectUser(login);
        }
    }
}