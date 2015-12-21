using System;
using System.Configuration;
using Saturn72.Core;
using Saturn72.Core.Configuration;

namespace Saturn72.Web.Framework.Plugins
{
    public class AreaSettings : ISettings
    {
        private const string AreaRelativePath = "~/Areas";
        private const string ShadowCopyRelativePath = "~/Areas/bin";

        public string ShadowCopyFolder { get; set; }

        public bool ClearAreasDirectoryOnStartup { get; set; }

        public string MainAreaFolder { get; set; }

        public static AreaSettings LoadStartupSettings()
        {
            return new AreaSettings
            {
                MainAreaFolder = CommonHelper.BuildRelativePath(AreaRelativePath),
                ClearAreasDirectoryOnStartup = GetClearShadowDirectoryOnStartupValue(),
                ShadowCopyFolder = CommonHelper.BuildRelativePath(ShadowCopyRelativePath)
            };
        }

        private static bool GetClearShadowDirectoryOnStartupValue()
        {
            return string.IsNullOrEmpty(ConfigurationManager.AppSettings["ClearAreasShadowDirectoryOnStartup"]) || 
                   Convert.ToBoolean(ConfigurationManager.AppSettings["ClearAreasShadowDirectoryOnStartup"]);
        }
    }
}