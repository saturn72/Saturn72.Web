using System.Web.Mvc;
using System.Web.Routing;
using Saturn72.Core.Modules;
using Saturn72.Web.Framework.Plugins;

namespace Saturn72.Web.Areas.Ui
{
    public class AdminArea : BaseModule, IAreaModule
    {
        public string AreaName => "Saturn72.Web.Areas.Ui";

        public void RegisterRoutes(RouteCollection routes)
        {
            //UI path
            routes.MapRoute("Default.UI",
                "{controller}/{action}/{id}",
                new {controller = "Dashboard", action = "Index", id = UrlParameter.Optional},
                new[] { "Saturn72.Web.Areas.Ui.Controllers" });
        }
        public int Priority => 100;
    }
}