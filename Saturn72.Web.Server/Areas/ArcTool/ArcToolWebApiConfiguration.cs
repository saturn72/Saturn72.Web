using System.Web.Http;
using Saturn72.Web.Framework.WebApi;

namespace Automation.Web.UI.Areas.ArcTool
{
    public class ArcToolWebApiConfiguration : IWebApiConfiguration
    {
        public int Order
        {
            get { return 100; }
        }

        public string Name
        {
            get { return "ArcTool"; }
        }

        public string RouteTemplate
        {
            get { return "arctool/api/{controller}/{id}"; }
        }

        public object Defaults
        {
            get { return new {id = RouteParameter.Optional}; }
        }

        public object Constraints
        {
            get { return null; }
        }
    }
}