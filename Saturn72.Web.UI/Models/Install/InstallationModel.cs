using Saturn72.Web.Framework.Mvc;

namespace Saturn72.Web.UI.Models.Install
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