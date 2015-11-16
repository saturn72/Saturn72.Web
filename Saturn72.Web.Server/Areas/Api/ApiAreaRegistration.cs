using System.Web.Mvc;

namespace Automation.Web.UI.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "ApiDefault",
               "Api/{controller}/{action}/{id}",
               new { controller = "Home", action = "Index", area = "Api", id = "" },
               new[] { "Automation.Web.UI.Areas.Api" }
           );
        }

        public override string AreaName
        {
            get { return "Api"; }
        }
    }
}