using System;
using System.Linq;
using System.Web.Mvc;
using Automation.Core.Domain.Job;
using Automation.Core.Services.Jobs;
using Automation.Core.Services.Localization;
using Automation.Extensions;
using Automation.Web.Framework.Controllers;
using Automation.Web.Framework.KendoUi;
using Automation.Web.UI.Areas.Server.Infrastructure;
using Automation.Web.UI.Areas.Server.Models.JobPlan;

namespace Automation.Web.UI.Areas.Server.Controllers
{
    public class AutomationJobPlanController : Saturn72ControllerBase
    {
        #region ctor

        public AutomationJobPlanController(IAutomationJobPlanService automationJobPlanService,
            IAutomationJobService automationJobService, ILocalizationService localizationService)
        {
            _automationJobPlanService = automationJobPlanService;
            _automationJobService = automationJobService;
            _localizationService = localizationService;
        }

        #endregion

        public ActionResult Plans(int id)
        {
            var model = new AutomationJobPlanListModel {Id = id};
            return View(model);
        }

        [HttpPost]
        public ActionResult GetAutomationJobPlanList(DataSourceRequest command, int id)
        {
            var jobPlans = _automationJobPlanService.GetAutomationJobPlansByAutomationJobId(id)
                .Select(aj => aj.ToModel());

            var gridModel = new DataSourceResult
            {
                Data = jobPlans,
                Total = jobPlans.Count()
            };

            return Json(gridModel);
        }

        public ActionResult Create(int id)
        {
            var autoJob = _automationJobService.GetAutomationJobById(id);
            var model = new AutomationJobPlanModel
            {
                AutomationJobId = autoJob.Id,
                AutomationJob = autoJob.Name
            };

            return View(model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(AutomationJobPlanModel model, bool continueEditing)
        {
            if (ValidateAllAutomationJobPlanModelProperties(model))
                return View(model);

            var autoJobPlan = model.ToEntity();

            autoJobPlan.CreatedOnUtc = DateTime.UtcNow;
            autoJobPlan.UpdatedOnUtc = DateTime.UtcNow;

            _automationJobPlanService.InsertAutomationJobPlan(autoJobPlan);

            SuccessNotification(_localizationService.GetResource("Automation.AutomationJobPlan.Added"));

            return continueEditing
                ? RedirectToAction("Edit", new {id = autoJobPlan.Id})
                : RedirectToAction("Plans", new {id = autoJobPlan.Id});
        }

        public ActionResult Edit(int id)
        {
            var autoJobPlan = _automationJobPlanService.GetAutomationJobPlanById(id);

            if (autoJobPlan == null || autoJobPlan.Deleted)
            {
                ErrorNotification(_localizationService.GetResource("Automation.AutomationJob.CannotEdit"));
                return RedirectToAction("Plans", new {id});
            }

            var model = autoJobPlan.ToModel();

            return View(model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(AutomationJobPlanModel model, bool continueEditing = false)
        {
            if (ValidateAllAutomationJobPlanModelProperties(model))
                return View(model);

            var autoJobPlan = _automationJobPlanService.GetAutomationJobPlanById(model.Id);
            if (autoJobPlan.IsNull() || autoJobPlan.Deleted)
            {
                ErrorNotification(_localizationService.GetResource("Automation.AutomationJob.CannotEdit"));
                return RedirectToAction("Plans", new {id = model.Id});
            }

            UpdateAutomationJobPlanFromModel(model, autoJobPlan);
            autoJobPlan.UpdatedOnUtc = DateTime.UtcNow;

            _automationJobPlanService.UpdateAutomationJobPlan(autoJobPlan);

            SuccessNotification(_localizationService.GetResource("Automation.AutomationJob.Updated"));
            return continueEditing
                ? RedirectToAction("Edit", new {id = autoJobPlan.Id})
                : RedirectToAction("Plans", new {id = model.AutomationJobId});
        }

        #region Fields

        private readonly IAutomationJobPlanService _automationJobPlanService;
        private readonly IAutomationJobService _automationJobService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Utilities

        private void UpdateAutomationJobPlanFromModel(AutomationJobPlanModel model, AutomationJobPlan autoJobPlan)
        {
            autoJobPlan.AutomationJobId = model.AutomationJobId;
            autoJobPlan.Name = model.Name;
            autoJobPlan.Comment = model.Comment;
            autoJobPlan.Description = model.Description;
            autoJobPlan.Enabled = model.Enabled;
        }

        private bool ValidateAllAutomationJobPlanModelProperties(AutomationJobPlanModel model)
        {
            return !ModelState.IsValid || AutomationJobPlanWithSamePropertiesExists(model);
        }

        private bool AutomationJobPlanWithSamePropertiesExists(AutomationJobPlanModel model)
        {
            var plans = _automationJobPlanService
                .GetAutomationJobPlansByAutomationJobId(model.AutomationJobId)
                .Where(ajp => ajp.Id != model.Id && !ajp.Deleted);

            var result = false;

            if (plans.Any(ajp => ajp.Name == model.Name))
                AddErrorNotificationAndSetResult("Automation.AutomationJobPlan.Fields.Name.Messages.AlreadyExists",
                    out result);

            if (plans.Any(ajp => ajp.Name == model.Description))
                AddErrorNotificationAndSetResult(
                    "Automation.AutomationJobPlan.Fields.Description.Messages.AlreadyExists", out result);

            return result;
        }


        private void AddErrorNotificationAndSetResult(string notificationKey, out bool result)
        {
            ErrorNotification(
                _localizationService.GetResource(notificationKey));
            result = true;
        }

        #endregion
    }
}