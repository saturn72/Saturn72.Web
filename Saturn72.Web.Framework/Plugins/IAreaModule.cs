using Saturn72.Core.Modules;
using Saturn72.Web.Framework.Routes;

namespace Saturn72.Web.Framework.Plugins
{
    public interface IAreaModule : IPlugin, IRouteProvider
    {
        string AreaName { get; }
    }
}