using System.Linq;
using Automation.Web.Framework.Controllers;
using Automation.Web.UI.Areas.ArcTool.Models.Rom;

namespace Automation.Web.UI.Areas.ArcTool.Controllers
{
    public class RomController:Saturn72ApiControllerBase
    {
        public ManufacturerModel[] GetAllManufacturers()
        {
           return new[]
            {
                new ManufacturerModel {Id = 1, Name = "Samsung"},
                new ManufacturerModel {Id = 2, Name = "LG"},
                new ManufacturerModel {Id = 3, Name = "HTC"}
            };
        }

        public ManufacturerModel[] GetModelsByManufacturerId(int id)
        {
            return new[]
            {
                new ManufacturerModel {Id = 1, Name = "Samsung1"},
                new ManufacturerModel {Id = 1, Name = "Samsung2"},
                new ManufacturerModel {Id = 1, Name = "Samsung3"},
                new ManufacturerModel {Id = 1, Name = "Samsung4"},
                new ManufacturerModel {Id = 2, Name = "LG1"},
                new ManufacturerModel {Id = 2, Name = "LG2"},
                new ManufacturerModel {Id = 2, Name = "LG3"},
                new ManufacturerModel {Id = 2, Name = "LG4"},
                new ManufacturerModel {Id = 3, Name = "HTC1"},
                new ManufacturerModel {Id = 3, Name = "HTC2"},
                new ManufacturerModel {Id = 3, Name = "HTC3"},
                new ManufacturerModel {Id = 3, Name = "HTC4"},
            }.Where(x => x.Id == id).ToArray();
        }
    }

   
}