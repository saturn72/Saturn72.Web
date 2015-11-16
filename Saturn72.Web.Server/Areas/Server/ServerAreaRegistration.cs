using System.Web.Http;
using System.Web.Mvc;

namespace Automation.Web.UI.Areas.Server
{
    public class ServerAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "ServerDefault",
               "Server/{controller}/{action}/{id}",
               new { controller="Home", action = "Index", area = "Server", id = RouteParameter.Optional },
               new[] { "Automation.Web.UI.Areas.Server.Controllers" }
           );
        }

        public override string AreaName
        {
            get { return "Server"; }
        }
    }
}