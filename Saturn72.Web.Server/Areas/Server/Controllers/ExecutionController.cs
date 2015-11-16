using System.Web.Mvc;
using Automation.Core.Services.Jobs;
using Automation.Web.Framework.Controllers;
using Automation.Web.UI.Areas.Server.Models.Execution;

namespace Automation.Web.UI.Areas.Server.Controllers
{
    public class ExecutionController : Saturn72ControllerBase
    {
        #region Fields

        private readonly IAutomationJobService _automationJobService;

        #endregion

        #region ctor

        public ExecutionController(IAutomationJobService automationJobService)
        {
            _automationJobService = automationJobService;
        }

        #endregion

        public ActionResult Run(int id)
        {
            var model = new ExecutionRequestModel();
            PreapareModel(model);
            return View(model);
        }

        #region Utilities

        private void PreapareModel(ExecutionRequestModel model)
        {
            var manufacturerGroup = new SelectListGroup {Name = "Manufacturers"};
            var modelGroup = new SelectListGroup {Name = "Models"};
            model.RomCascadeListModel = new RomCascadeListModel
            {
                AvailableManufacturers = new[]
                {
                    new SelectListItem {Text = "Select manufacturer...", Value = "0", Group = manufacturerGroup},
                    new SelectListItem {Text = "Samsung", Value = "1", Group = manufacturerGroup},
                    new SelectListItem {Text = "LG", Value = "2", Group = manufacturerGroup},
                    new SelectListItem {Text = "HTC", Value = "3", Group = manufacturerGroup}
                },
                AvailableManufacturerModels = new[]
                {
                    new SelectListItem {Text = "Select model...", Value = "0", Group = modelGroup},
                    new SelectListItem {Text = "SamsungModel1", Value = "1", Group = modelGroup},
                    new SelectListItem {Text = "SamsungModel2", Value = "2", Group = modelGroup},
                    new SelectListItem {Text = "SamsungModel3", Value = "3", Group = modelGroup},
                    new SelectListItem {Text = "SamsungModel4", Value = "4", Group = modelGroup},
                    new SelectListItem {Text = "SamsungModel5", Value = "5", Group = modelGroup},
                    new SelectListItem {Text = "LGModel1", Value = "6", Group = modelGroup},
                    new SelectListItem {Text = "LGModel2", Value = "7", Group = modelGroup},
                    new SelectListItem {Text = "LGModel3", Value = "8", Group = modelGroup},
                    new SelectListItem {Text = "LGModel4", Value = "9", Group = modelGroup},
                    new SelectListItem {Text = "HtcModel1", Value = "10", Group = modelGroup},
                    new SelectListItem {Text = "HtcModel2", Value = "11", Group = modelGroup},
                    new SelectListItem {Text = "HtcModel3", Value = "12", Group = modelGroup}
                }
            };
        }

        #endregion
    }
}