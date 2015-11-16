using System.Web.Mvc;
using System.Web.Routing;
using Automation.Web.Framework.Routes;

namespace Automation.Web.UI.Areas.Server
{
    public class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Server.Api.Ui",
                "Server/Api/Ui",
                new {controller = "Ui", action = "Index"},
                new[] {"Automation.Web.UI.Areas.Server.Controllers"});
        }

        public int Priority
        {
            get { return 100; }
        }
    }
}