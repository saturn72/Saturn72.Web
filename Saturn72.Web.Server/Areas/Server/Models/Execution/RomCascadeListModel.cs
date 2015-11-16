using System.Collections.Generic;
using System.Web.Mvc;
using Automation.Web.Framework.Mvc;

namespace Automation.Web.UI.Areas.Server.Models.Execution
{
    public class RomCascadeListModel:BaseSaturn72Model
    {
        public int ManufacturerId { get; set; }
        public IEnumerable<SelectListItem> AvailableManufacturers { get; set; }

        public int ManufacturerModelId { get; set; }
        public IEnumerable<SelectListItem> AvailableManufacturerModels { get; set; }
    }
}