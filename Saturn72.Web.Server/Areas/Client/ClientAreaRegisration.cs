using System.Web.Mvc;

namespace Automation.Web.UI.Areas.Client
{
    public class ClientAreaRegisration:AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "ClientDefault",
               "Client/{controller}/{action}/{id}",
               new { controller = "Home", action = "Index", area = "Client", id = "" },
               new[] { "Automation.Web.UI.Areas.Client.Controllers" }
           );
        }

        public override string AreaName
        {
            get { return "Client"; }
        }
    }
}