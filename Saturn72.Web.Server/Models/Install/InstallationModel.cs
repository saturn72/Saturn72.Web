using Automation.Web.Framework.Mvc;

namespace Automation.Web.UI.Models.Install
{
    public class InstallationModel:BaseSaturn72Model
    {
        public InstallationModel()
        {
            InstallDefaultLanguageResource = true;
        }
        public bool InstallDefaultLanguageResource { get; set; }
    }
}