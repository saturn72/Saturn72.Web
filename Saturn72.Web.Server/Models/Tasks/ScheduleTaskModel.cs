using System.Web.Mvc;
using Automation.Web.Framework;
using Automation.Web.Framework.Mvc;
using Automation.Web.UI.Validators.Tasks;
using FluentValidation.Attributes;

namespace Automation.Web.UI.Models.Tasks
{
    [Validator(typeof (ScheduleTaskValidator))]
    public class ScheduleTaskModel : BaseSaturn72EntityModel
    {
        [Saturn72ResourceDisplayName("Admin.System.ScheduleTasks.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ScheduleTasks.Fields.Seconds")]
        public int Seconds { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ScheduleTasks.Fields.Enabled")]
        public bool Enabled { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ScheduleTasks.Fields.StopOnError")]
        public bool StopOnError { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ScheduleTasks.Fields.LastStart")]
        public string LastStartUtc { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ScheduleTasks.Fields.LastEnd")]
        public string LastEndUtc { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ScheduleTasks.Fields.LastSuccess")]
        public string LastSuccessUtc { get; set; }
    }
}