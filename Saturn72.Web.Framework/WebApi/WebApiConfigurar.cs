using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Saturn72.Core.Infrastructure;
using Saturn72.Extensions;

namespace Saturn72.Web.Framework.WebApi
{
    public class WebApiConfigurar : IWebApiConfigurar
    {
        public void Configure()
        {
            var apiConfigs = new List<IWebApiConfiguration>();
            var typeFinder = EngineContext.Current.Resolve<ITypeFinder>();
            typeFinder.FindClassesOfType<IWebApiConfiguration>()
                .ForEachItem(ac =>
                    apiConfigs.Add((IWebApiConfiguration) Activator.CreateInstance(ac)));

            GlobalConfiguration.Configure(config =>
            {
                config.MapHttpAttributeRoutes();

                foreach (var apiConfig in apiConfigs.OrderBy(ac=>ac.Order))
                    config.Routes.MapHttpRoute(apiConfig.Name, apiConfig.RouteTemplate, apiConfig.Defaults,
                        apiConfig.Constraints);
            });
        }
    }
}