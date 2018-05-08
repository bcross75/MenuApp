using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using MenuApp.Data;

namespace MenuApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
            container.Register(
                Classes.FromThisAssembly().Where(x => x.Name.EndsWith("Controller")).LifestylePerWebRequest(),
                Component.For<IMenuRepository>() .ImplementedBy<MenuRepository>().LifestylePerWebRequest()
            );
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container.Kernel));

        }
    }
}
