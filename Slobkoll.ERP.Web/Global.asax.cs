using Ninject;
using Ninject.Web.Common.WebHost;
using Slobkoll.ERP.Core;
using Slobkoll.ERP.Core.Repository.Implementation;
using Slobkoll.ERP.Core.Repository.Interface;
using Slobkoll.ERP.Web.Providers.Implementation;
using Slobkoll.ERP.Web.Providers.Interface;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Slobkoll.ERP.Web
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new RepositoryModule());

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IGroupRepository>().To<GroupRepository>();
            kernel.Bind<ITaskRepository>().To<TaskRepository>();
            kernel.Bind<ISubTaskRepository>().To<SubTaskRepository>();
            kernel.Bind<ICommentRepository>().To<CommentRepository>();
            kernel.Bind<IHomeProvider>().To<HomeProvider>();
            kernel.Bind<IAuthProvider>().To<AuthProvider>();
            kernel.Bind<IAdminProvider>().To<AdminProvider>();
            
            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            base.OnApplicationStarted();
        }
    }
}
