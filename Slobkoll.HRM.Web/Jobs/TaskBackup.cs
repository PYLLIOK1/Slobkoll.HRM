using Quartz;
using Slobkoll.HRM.Web.Providers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Slobkoll.HRM.Web.Jobs
{
    public class TaskBackup : IJob
    {
        private readonly IJobProvider _jobProvider;
        public TaskBackup(IJobProvider jobProvider)
        {
            _jobProvider = jobProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            {
                await Task.Run(() => _jobProvider.JobBackup());
            }
        }
    }
}