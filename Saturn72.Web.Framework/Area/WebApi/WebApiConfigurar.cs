using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Saturn72.Core.Infrastructure;
using Saturn72.Extensions;

namespace Saturn72.Web.Framework.Area.WebApi
{
    public class WebApiConfigurar : IWebApiConfigurar
    {
        public void Configure()
        {
            var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
            var findClassesOfType = typeFinder.FindClassesOfType<IWebApiConfiguration>();

            var apiConfigs = new List<IWebApiConfiguration>();
            findClassesOfType.ForEachItem(ac =>
                    apiConfigs.Add((IWebApiConfiguration) Activator.CreateInstance(ac)));

            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();

                if (apiConfigs.IsEmpty())
                    return;

                foreach (var apiConfig in apiConfigs.OrderBy(ac=>ac.ConfigureOrder))
                    config.Routes.MapHttpRoute(apiConfig.Name, apiConfig.RouteTemplate, apiConfig.Defaults,
                        apiConfig.Constraints);
            });
        }
    }
}