using Automation.Core.Domain.Client;
using Automation.Core.Services.Client;
using Automation.Web.Framework.Controllers;

namespace Automation.Web.UI.Areas.Api.Controllers
{
    public class ClientDataController : Saturn72ApiControllerBase
    {
        private readonly IClientMachineService _clientMachineExecutionService;

        public ClientDataController(IClientMachineService clientMachineExecutionService)
        {
            _clientMachineExecutionService = clientMachineExecutionService;
        }

        // GET: api/ExecutionApi/{clientId} 
        public ClientMachineExecutionData Get(int id)
        {
            return _clientMachineExecutionService.GetClientMachineExecutionDataByClientId(id);
        }
    }
}