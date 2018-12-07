using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Tool.hbm2ddl;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using Slobkoll.ERP.Core.Object;

namespace Slobkoll.ERP.Core
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISessionFactory>()
                .ToMethod
                (
                    e =>
                        Fluently.Configure()
                        .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c =>
                            c.FromConnectionStringWithKey("SedDbConnString")))
                        .Cache(c => c.UseQueryCache().ProviderClass<HashtableCacheProvider>())
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<User>().AddFromAssemblyOf<Task>()
                        .AddFromAssemblyOf<SubTask>().AddFromAssemblyOf<Comment>()
                        .AddFromAssemblyOf<Group>())
                        .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, false, false))
                        .BuildConfiguration()
                        .BuildSessionFactory()
                )
                .InSingletonScope();

            Bind<ISession>()
                .ToMethod((ctx) => ctx.Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();
        }
    }
}
