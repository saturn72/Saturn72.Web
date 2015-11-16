using System.Web.Mvc;
using Automation.Core;
using Automation.Core.Services.Localization;
using Automation.Web.Framework.Controllers;
using Automation.Web.Framework.Services.Install;
using Automation.Web.UI.Models.Install;

namespace Automation.Web.UI.Controllers
{
    public class InstallController:Saturn72ControllerBase
    {
        private readonly ILocalizationService _localizationService;
        private readonly IInstallationService _installationService;

        public InstallController(ILocalizationService localizationService, IInstallationService installationService)
        {
            _localizationService = localizationService;
            _installationService = installationService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Install");
        }

        public ActionResult Install()
        {
            var model = new InstallationModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Install(InstallationModel model)
        {
            if (!ModelState.IsValid)
            {
                ErrorNotification(_localizationService.GetResource("Automation.Install.Message.CannotInstallResources"));
                return View(model);
            }
            _installationService.InstallAllResources(CommonHelper.BuildRelativePath("~/App_Data/Installation"));
            SuccessNotification(_localizationService.GetResource("Automation.Installation.Message.AllInstalled"));
            return RedirectToAction("Index", "Home");
        }
    }
}