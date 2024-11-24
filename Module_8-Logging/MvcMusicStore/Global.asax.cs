using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using MvcMusicStore.Controllers;
using NLog;

namespace MvcMusicStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ILogger logger;
        public MvcApplication()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            // Need to register types where the logger will be used.
            builder.RegisterControllers(typeof(HomeController).Assembly);
            builder.RegisterControllers(typeof(AccountController).Assembly);

            // Registering the logger with the controller classes(?)
            builder.Register(f => LogManager.GetLogger("Common Logger")).As<ILogger>();
            var container = builder.Build();

            // Apparently this somehow takes care of some dependencies.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            logger.Info("Application started");
        }

        // Logs exceptions into the log file.
        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            logger.Error(ex.ToString());
        }
    }
}
