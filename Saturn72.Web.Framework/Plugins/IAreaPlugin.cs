using Saturn72.Core.Plugins;
using Saturn72.Web.Framework.Routes;

namespace Saturn72.Web.Framework.Plugins
{
    public interface IAreaPlugin : IPlugin, IRouteProvider
    {
        string AreaName { get; }
    }
}