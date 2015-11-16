using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Automation.Core.Domain.Logging;
using Automation.Core.Infrastructure;
using Automation.Core.Services.Tasks;
using Automation.Web.Framework;
using Automation.Web.Framework.Mvc;
using Automation.Web.Framework.Routes;
using Automation.Web.Framework.ViewEngines;
using FluentValidation.Mvc;
using Saturn72.Web.Framework.WebApi;

namespace Automation.Web.UI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            EngineContext.Initialize(false);

            SetViewEngine();

            ModelMetadataProviders.Current = new Saturn72MetadataProvider();

            //api must be the first to registered
            ConfigureApiArea();
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes();

            //fluent validation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(new Saturn72ValidatorFactory()));

            StartTaskManager();
            try
            {
                //log
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Information("Application started", null, null);
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }

        protected virtual void ConfigureApiArea()
        {
            var webApiConfigurar = EngineContext.Current.Resolve<IWebApiConfigurar>();
            webApiConfigurar.Configure();            
        }

        private static void StartTaskManager()
        {
            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();
        }

        private static void SetViewEngine()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new Saturn72ViewEngine());
        }

        private void RegisterRoutes()
        {
            var routes = RouteTable.Routes;
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //register custom routes (plugins, etc)
            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional},
                new[] {"Automation.Web.UI.Controllers"}
                );
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //TODO: Complete on_Error functinality
            var exception = Server.GetLastError();
            throw exception;
        }
    }
}