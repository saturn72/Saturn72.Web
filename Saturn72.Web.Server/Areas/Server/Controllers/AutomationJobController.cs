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
using Automation.Web.UI.Areas.Server.Models.Job;

namespace Automation.Web.UI.Areas.Server.Controllers
{
    public class AutomationJobController : Saturn72ControllerBase
    {
        #region ctor

        public AutomationJobController(IAutomationJobService automationJobService,
            ILocalizationService localizationService)
        {
            _automationJobService = automationJobService;
            _localizationService = localizationService;
        }

        #endregion

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new AutomationJobListModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult GetAutomationJobList(DataSourceRequest command)
        {
            var jobs = _automationJobService.GetAll(true).Select(x => x.ToModel());

            var gridModel = new DataSourceResult
            {
                Data = jobs,
                Total = jobs.Count()
            };

            return Json(gridModel);
        }

        public ActionResult Create()
        {
            var model = new AutomationJobModel();
            return View(model);
        }

        [HttpPost]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(AutomationJobModel model, bool continueEditing)
        {
            if (!ModelState.IsValid || ValidateAllProperties(model))
                return View(model);

            var testCase = model.ToEntity();

            testCase.CreatedOnUtc = DateTime.UtcNow;
            testCase.UpdatedOnUtc = DateTime.UtcNow;

            _automationJobService.InsertAutomationJob(testCase);

            SuccessNotification(_localizationService.GetResource("Automation.AutomationJob.Added"));

            return continueEditing ? RedirectToAction("Edit", new {id = testCase.Id}) : RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            var testCase = _automationJobService.GetAutomationJobById(id);

            if (testCase == null || testCase.Deleted)
            {
                ErrorNotification(_localizationService.GetResource("Automation.AutomationJob.CannotEdit"));
                return RedirectToAction("List");
            }

            var model = testCase.ToModel();

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(AutomationJobModel model, bool continueEditing = false)
        {
            if (!ModelState.IsValid || AutomationJobWithSamePropertiesExists(model))
                return View(model);

            var autoJob = _automationJobService.GetAutomationJobById(model.Id);
            if (autoJob.IsNull() || autoJob.Deleted)
            {
                ErrorNotification(_localizationService.GetResource("Automation.AutomationJob.CannotEdit"));
                return RedirectToAction("List");
            }

            UpdateAutomationJobFromModel(model, autoJob);
            autoJob.UpdatedOnUtc = DateTime.UtcNow;

            _automationJobService.UpdateAutomationJob(autoJob);

            SuccessNotification(_localizationService.GetResource("Automation.AutomationJob.Updated"));
            return continueEditing ? RedirectToAction("Edit", new {id = autoJob.Id}) : RedirectToAction("List");
        }

        #region Fields

        private readonly IAutomationJobService _automationJobService;
        private readonly ILocalizationService _localizationService;

        #endregion

        #region Utilities

        private bool ValidateAllProperties(AutomationJobModel model)
        {
            return ValidateModelProperties(model) && AutomationJobWithSamePropertiesExists(model);
        }

        private bool ValidateModelProperties(AutomationJobModel model)
        {
            if (model.Guid != Guid.Empty)
                return true;

            ErrorNotification(_localizationService.GetResource("Automation.AutomationJob.Fields.Guid.Messages.NotEmpty"));
            return false;
        }

        private bool AutomationJobWithSamePropertiesExists(AutomationJobModel model)
        {
            var jobs = _automationJobService.GetAll().Where(tc => tc.Id != model.Id && !tc.Deleted);
            var result = false;

            if (jobs.Any(aj => aj.Guid == model.Guid))
                AddErrorNotificationAndSetResult("Automation.AutomationJob.Fields.Guid.Messages.AlreadyExists",
                    out result);

            if (jobs.Any(aj => aj.DisplayName == model.DisplayName))
                AddErrorNotificationAndSetResult("Automation.AutomationJob.Fields.DisplayName.Messages.AlreadyExists",
                    out result);

            if (jobs.Any(aj => aj.DisplayName == model.DisplayName))
                AddErrorNotificationAndSetResult("Automation.AutomationJob.Fields.DisplayName.Messages.AlreadyExists",
                    out result);

            if (jobs.Any(aj => aj.Name == model.Name))
                AddErrorNotificationAndSetResult("Automation.AutomationJob.Fields.Name.Messages.AlreadyExists",
                    out result);

            return result;
        }

        private void AddErrorNotificationAndSetResult(string notificationKey, out bool result)
        {
            ErrorNotification(
                _localizationService.GetResource(notificationKey));
            result = true;
        }

        private void UpdateAutomationJobFromModel(AutomationJobModel model, AutomationJob automationJob)
        {
            automationJob.Enabled = model.Enabled;
            automationJob.Published = model.Published || automationJob.Enabled;
            automationJob.Comment = model.Comment;
            automationJob.DisplayName = model.DisplayName;
            automationJob.Description = model.Description;
            automationJob.Name = model.Name;
        }

        #endregion
    }
}