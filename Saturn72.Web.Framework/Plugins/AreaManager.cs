using System.Web;
using Saturn72.Core.Plugins;
using Saturn72.Web.Framework.Plugins;

[assembly: PreApplicationStartMethod(typeof (AreaManager), "Initialize")]
namespace Saturn72.Web.Framework.Plugins
{
    public class AreaManager
    {
        public static void Initialize()
        {
            var areaSettings = AreaSettings.LoadStartupSettings();
            PluginManager.InitializeWithParameters(areaSettings.MainAreaFolder, areaSettings.ShadowCopyFolder,
                areaSettings.ClearAreasDirectoryOnStartup);
        }
    }
}