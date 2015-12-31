using System.Web.Mvc;
using System.Web.Routing;
using Saturn72.Core.Plugins;
using Saturn72.Web.Framework.Plugins;

namespace Saturn72.Web.Areas.Admin
{
    public class AdminArea : BasePlugin, IAreaPlugin
    {
        public string AreaName => "Saturn72.Web.Areas.Admin";

        public void RegisterRoutes(RouteCollection routes)
        {
            //UI path
            routes.MapRoute("Default.Admin.UI",
                "admin/{controller}/{action}/{id}",
                new {controller = "Dashboard", action = "Index", id = UrlParameter.Optional},
                new[] { "Saturn72.Web.Areas.Admin.Controllers" });
        }
        public int Priority => 100;
    }
}