using System.Web.Mvc;
using Saturn72.Web.UI.Validators.Tasks;
using FluentValidation.Attributes;
using Saturn72.Web.Framework;
using Saturn72.Web.Framework.Mvc;

namespace Saturn72.Web.UI.Models.Tasks
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