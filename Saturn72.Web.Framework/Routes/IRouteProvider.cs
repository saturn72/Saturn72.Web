using System.Web.Routing;

namespace Saturn72.Web.Framework.Routes
{
    public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes);

        int Priority { get; }
    }
}