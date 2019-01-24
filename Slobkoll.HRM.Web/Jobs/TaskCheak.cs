using Quartz;
using Slobkoll.HRM.Web.Providers.Interface;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Slobkoll.HRM.Web.Jobs
{
    public class TaskCheak : IJob
    {
        private readonly IJobProvider _jobProvider;
        public TaskCheak(IJobProvider jobProvider)
        {
            _jobProvider = jobProvider;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            {
                await Task.Run(() => _jobProvider.JobTaskCheak());
            }  
        }
    }
}