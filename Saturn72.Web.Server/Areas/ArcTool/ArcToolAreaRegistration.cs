using System.Web.Http;
using System.Web.Mvc;

namespace Automation.Web.UI.Areas.ArcTool
{
    public class ArcToolAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "ArcToolDefault",
               "ArcTool/{controller}/{action}/{id}",
               new { id = RouteParameter.Optional },
               new[] { "Automation.Web.UI.Areas.ArcTool.Controllers" }
           );
        }

        public override string AreaName
        {
            get { return "ArcTool"; }
        }
    }
}