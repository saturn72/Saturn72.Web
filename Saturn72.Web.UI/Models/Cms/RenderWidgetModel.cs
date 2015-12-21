using System.Web.Routing;
using Automation.Web.Framework.Mvc;

namespace Automation.Web.UI.Models.Cms
{
    public class RenderWidgetModel : BaseSaturn72Model
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
    }
}