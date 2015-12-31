using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation.Mvc;
using Owin;
using Saturn72.Core.Infrastructure;
using Saturn72.Core.Logging;
using Saturn72.Core.Services.Tasks;
using Saturn72.Web.Framework;
using Saturn72.Web.Framework.Mvc;
using Saturn72.Web.Framework.Owin;
using Saturn72.Web.Framework.Routes;
using Saturn72.Web.Framework.ViewEngines;

namespace Saturn72.Web.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            EngineContext.Initialize(false);
            SetViewEngine();
            ModelMetadataProviders.Current = new Saturn72MetadataProvider();
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes();
            ConfigureWebApiUsingOwin(appBuilder);

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(
                new FluentValidationModelValidatorProvider(new Saturn72ValidatorFactory()));

            StartTaskManager();

            var logger = EngineContext.Current.Resolve<ILogger>();
            logger.Information("Application started");
        }

        private static void StartTaskManager()
        {
            EngineContext.Current.Resolve<ITaskManager>().Initialize();
        }

        private static void ConfigureWebApiUsingOwin(IAppBuilder appBuilder)
        {
            ConfigureWebApiViaOwin(appBuilder);
            ConfigureOtherOwinComponents(appBuilder);
        }

        private static void ConfigureOtherOwinComponents(IAppBuilder appBuilder)
        {
            var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
            var ocTypes = typeFinder.FindClassesOfType<IOwinConfigurar>();

            var ocInstances = new List<IOwinConfigurar>();
            foreach (var drType in ocTypes)
                ocInstances.Add((IOwinConfigurar) Activator.CreateInstance(drType));

            //sort
            ocInstances = ocInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var owinConfigurar in ocInstances)
                owinConfigurar.Configure(appBuilder);
        }

        private static void ConfigureWebApiViaOwin(IAppBuilder appBuilder)
        {
            var config = ConfigureApi();

            appBuilder.UseAutofacMiddleware(EngineContext.Current.ContainerManager.Container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }

        private static void RegisterRoutes()
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
                new[] {"Saturn72.Web.UI.Controllers"}
                );
        }

        private static HttpConfiguration ConfigureApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
                );
            return config;
        }

        private static void SetViewEngine()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new Saturn72ViewEngine());
        }
    }
}