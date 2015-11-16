using System;
using System.Web.Mvc;
using Automation.Web.Framework;
using Automation.Web.Framework.Mvc;

namespace Automation.Web.UI.Models.ClientMachine
{
    public class ClientMachineModel:BaseSaturn72EntityModel
    {
        [Saturn72ResourceDisplayName("Admin.System.ClientMachine.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ClientMachine.Fields.IpAddress")]
        [AllowHtml]
        public string IpAddress { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ClientMachine.Fields.Active")]
        public bool Active { get; set; }

        [Saturn72ResourceDisplayName("Admin.System.ClientMachine.Fields.LastConnectionOn")]
        public DateTime? LastConnectionOn { get; set; }
    }
}