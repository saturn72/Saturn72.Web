using System.Web.Mvc;
using Automation.Web.Framework.Controllers;

namespace Automation.Web.UI.Areas.Server.Controllers
{
    public class HomeController:Saturn72ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}