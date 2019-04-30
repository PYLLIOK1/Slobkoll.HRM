using Ninject;
using Ninject.Syntax;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Slobkoll.HRM.Core;
using Slobkoll.HRM.Core.Repository.Implementation;
using Slobkoll.HRM.Core.Repository.Interface;
using Slobkoll.HRM.Web.Providers.Implementation;
using Slobkoll.HRM.Web.Providers.Interface;

namespace Slobkoll.HRM.Web.Jobs
{
    public class Scheduler
    {
        public static async void Start()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(new RepositoryModule());
            kernel.Bind<IJobProvider>().To<JobProvider>();
            kernel.Bind<ITaskRepository>().To<TaskRepository>();
            kernel.Bind<ISubTaskRepository>().To<SubTaskRepository>();

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = new NinjectJobFactory(kernel);

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<TaskCheak>().Build();

            ITrigger trigger = TriggerBuilder.Create()  
                .WithIdentity("trigger1", "group1")     
                .StartNow()                            
                .WithSimpleSchedule(x => x            
                    .WithIntervalInMinutes(1)          
                    .RepeatForever())                   
                .Build();                               
                             

            await scheduler.ScheduleJob(job, trigger);        
        }
    }

    internal class NinjectJobFactory : IJobFactory
    {
        private readonly IResolutionRoot resolutionRoot;

        public NinjectJobFactory(IResolutionRoot resolutionRoot)
        {
            this.resolutionRoot = resolutionRoot;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)this.resolutionRoot.Get(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            this.resolutionRoot.Release(job);
        }
    }
}