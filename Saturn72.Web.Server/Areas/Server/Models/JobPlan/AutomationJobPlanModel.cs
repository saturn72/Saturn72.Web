using System;
using System.Web.Mvc;
using Automation.Web.Framework;
using Automation.Web.Framework.Mvc;

namespace Automation.Web.UI.Areas.Server.Models.JobPlan
{
    public class AutomationJobPlanModel : BaseSaturn72EntityModel
    {
        [Saturn72ResourceDisplayName("Automation.AutomationJobPlan.Fields.CreatedOnUtc")]
        public DateTime CreatedOnUtc { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJobPlan.Fields.UpdatedOnUtc")]
        public DateTime UpdatedOnUtc { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJobPlan.Fields.Enabled")]
        public bool Enabled { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJobPlan.Fields.IsDefault")]
        public bool IsDefault { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJobPlan.Fields.Name")]
        public string Name { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJobPlan.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJobPlan.Fields.Comment")]
        public string Comment { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJobPlan.Fields.AutomationJob")]
        public int AutomationJobId { get; set; }

        public string AutomationJob { get; set; }
    }
}