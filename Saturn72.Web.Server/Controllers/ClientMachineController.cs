using System;
using System.Linq;
using System.Web.Mvc;
using Automation.Core.Domain.Client;
using Automation.Core.Services.Client;
using Automation.Core.Services.Localization;
using Automation.Extensions;
using Automation.Web.Framework.Controllers;
using Automation.Web.Framework.KendoUi;
using Automation.Web.Framework.Mvc;
using Automation.Web.UI.Areas.Server.Infrastructure;
using Automation.Web.UI.Models.ClientMachine;

namespace Automation.Web.UI.Controllers
{
    public class ClientMachineController : Saturn72ControllerBase
    {
        private readonly IClientMachineService _clientMachineService;
        private readonly ILocalizationService _localizationService;

        public ClientMachineController(IClientMachineService clientMachineService,
            ILocalizationService localizationService)
        {
            _clientMachineService = clientMachineService;
            _localizationService = localizationService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new ClientMachineListModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command)
        {
            var models = _clientMachineService.GetAllClientMachines()
                .Select(PrepareScheduleTaskModel)
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult ClientMachineAdd([Bind(Exclude = "Id,LastConnectionOn")] ClientMachineModel model)
        {
            if (!ModelState.IsValid)
                return Json(new DataSourceResult {Errors = ModelState.SerializeErrors()});

            var clientMachine = model.ToEntity();
            clientMachine.CreatedOn = DateTime.UtcNow;
            clientMachine.UpdatedOn = DateTime.UtcNow;

            _clientMachineService.InsertClientMachine(clientMachine);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult Update(ClientMachine model)
        {
            if (!ModelState.IsValid)
                return Json(new DataSourceResult { Errors = ModelState.SerializeErrors() });

            var clientMachine = _clientMachineService.GetClientMachineById(model.Id);
            if (clientMachine.IsNull())
                return
                    Json(new DataSourceResult
                    {
                        Errors = _localizationService.GetResource("Admin.System.ClientMachine.NotExists")
                    });

            CopyClientMachineModelToClientMachineEntity(model, clientMachine);

            _clientMachineService.UpdateClientMachine(clientMachine);
            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult Delete(ClientMachine model)
        {
            if (!ModelState.IsValid)
                return Json(new DataSourceResult { Errors = ModelState.SerializeErrors() });

            var clientMachine = _clientMachineService.GetClientMachineById(model.Id);
            if (clientMachine.IsNull())
                return
                    Json(new DataSourceResult
                    {
                        Errors = _localizationService.GetResource("Admin.System.ClientMachine.NotExists")
                    });

            _clientMachineService.DeleteClientMachine(clientMachine);
            return new NullJsonResult();
        }


        #region Utility

        [NonAction]
        protected virtual ClientMachineModel PrepareScheduleTaskModel(ClientMachine clientMachine)
        {
            return new ClientMachineModel
            {
                Id = clientMachine.Id,
                Name = clientMachine.Name,
                Active = clientMachine.Active,
                IpAddress = clientMachine.IpAddress,
                LastConnectionOn = clientMachine.LastConnectionOn
            };
        }

        private static void CopyClientMachineModelToClientMachineEntity(ClientMachine model, ClientMachine clientMachine)
        {
            clientMachine.Active = model.Active;
            clientMachine.CreatedOn = model.CreatedOn;
            clientMachine.IpAddress = model.IpAddress;
            clientMachine.LastConnectionOn = model.LastConnectionOn;
            clientMachine.Name = model.Name;
            clientMachine.UpdatedOn = DateTime.UtcNow;
        }
        #endregion
    }
}