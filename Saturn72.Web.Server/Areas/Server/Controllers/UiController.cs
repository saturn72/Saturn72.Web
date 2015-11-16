using Automation.Web.Framework.Controllers;
using Automation.Web.Framework.KendoUi.Components.Menu;

namespace Automation.Web.UI.Areas.Server.Controllers
{
    public class UiController : Saturn72ApiControllerBase
    {
        public MenuItem[] GetMainMenu()
        {
            return new[]
            {
                new MenuItem {Text = "MenuItem1"},
                new MenuItem {Text = "MenuItem1"},
                new MenuItem {Text = "MenuItem1"},
                new MenuItem {Text = "MenuItem1"}
            };
        }
    }
}