using System;
using System.Web.Mvc;
using Automation.Web.Framework;
using Automation.Web.Framework.Mvc;
using Automation.Web.UI.Areas.Server.Validators.Job;
using FluentValidation.Attributes;

namespace Automation.Web.UI.Areas.Server.Models.Job
{
    [Validator(typeof(AutomationJobValidator))]
    public class AutomationJobModel : BaseSaturn72EntityModel
    {
        [AllowHtml]
        [Saturn72ResourceDisplayName("Automation.AutomationJob.Fields.Name")]
        public string Name { get; set; }
        [AllowHtml]
        [Saturn72ResourceDisplayName("Automation.AutomationJob.Fields.DisplayName")]
        public string DisplayName { get; set; }

        [AllowHtml]
        [Saturn72ResourceDisplayName("Automation.AutomationJob.Fields.Description")]
        public string Description { get; set; }

        [AllowHtml]
        [Saturn72ResourceDisplayName("Automation.AutomationJob.Fields.Comment")]
        public string Comment { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJob.Fields.Enabled")]
        public bool Enabled { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJob.Fields.Published")]
        public bool Published { get; set; }

        [Saturn72ResourceDisplayName("Automation.AutomationJob.Fields.Guid")]
        public Guid Guid { get; set; }
    }
}