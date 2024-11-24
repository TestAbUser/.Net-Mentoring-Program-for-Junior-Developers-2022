using Microsoft.Owin;
using MvcMusicStore.Infrastructure;
using Owin;
using PerformanceCounterHelper;
using System.Diagnostics;

[assembly: OwinStartupAttribute(typeof(MvcMusicStore.Startup))]
namespace MvcMusicStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Check that the required counter category is not registered before installing it.
            // MUST RUN THE APP IN ADMIN MODE!
            if (!PerformanceCounterCategory.Exists("Mvc Music Store Counters"))
            {
                PerformanceHelper.Install(typeof(Counters));
            }
            ConfigureAuth(app);

            ConfigureApp(app);
        }
    }
}
