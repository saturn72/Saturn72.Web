using System.Web.Mvc;
using Saturn72.Web.Framework.Controllers;

namespace Saturn72.Web.Areas.Admin.Controllers
{
    public class DashboardController : Saturn72ControllerBase
    {
        protected override string ControllerMainViewPath => "~/Areas/Saturn72.Web.Areas.Admin/Views/Dashboard";

        public ActionResult Index()
        {
            return GetView();
        }
    }
}