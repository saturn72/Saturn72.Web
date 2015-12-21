using System.Linq;
using System.Web.Mvc;
using Saturn72.Core.Domain.Tasks;
using Saturn72.Core.Services.Tasks;
using Saturn72.Extensions;
using Saturn72.Web.Framework.Controllers;
using Saturn72.Web.Framework.KendoUi;
using Saturn72.Web.UI.Models.Tasks;

namespace Saturn72.Web.UI.Controllers
{
    public class ScheduleTaskController : Saturn72ControllerBase
    {
        private readonly IScheduleTaskService _scheduleTaskService;

        public ScheduleTaskController(IScheduleTaskService scheduleTaskService)
        {
            _scheduleTaskService = scheduleTaskService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command)
        {
            var models = _scheduleTaskService.GetAllTasks(true)
                .Select(PrepareScheduleTaskModel)
                .ToList();
            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };

            return Json(gridModel);
        }

        #region Utility

        [NonAction]
        protected virtual ScheduleTaskModel PrepareScheduleTaskModel(ScheduleTask task)
        {
            return new ScheduleTaskModel
            {
                Id = task.Id,
                Name = task.Name,
                Seconds = task.Seconds,
                Enabled = task.Enabled,
                StopOnError = task.StopOnError,
                LastStartUtc = task.LastStartUtc.NullableDateTimeToString(),
                LastEndUtc = task.LastEndUtc.NullableDateTimeToString(),
                LastSuccessUtc = task.LastSuccessUtc.NullableDateTimeToString()
            };
        }

        #endregion

        protected override string ControllerMainViewPath => "~/Saturn72.Web.UI/Views/ScheduleTask";
    }
}