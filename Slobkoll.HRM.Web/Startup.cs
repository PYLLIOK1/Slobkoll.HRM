using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Slobkoll.HRM.Web.Startup))]

namespace Slobkoll.HRM.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
