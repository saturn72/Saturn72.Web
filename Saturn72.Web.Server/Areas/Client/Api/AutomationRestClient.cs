using System.Linq;
using Automation.Core.Domain.Client;
using Automation.Core.Net.Rest;
using Automation.Extensions;

namespace Automation.Web.UI.Areas.Client.Api
{
    public class AutomationRestClient : IAutomationRestClient
    {
        private readonly IRestClient _restClient;

        public AutomationRestClient(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public ClientMachineExecutionData GetClientMachineExecutionData(int clientId)
        {
            return _restClient.SubmitGetRequest<ClientMachineExecutionData>("ClientData/{0}".AsFormat(clientId)).FirstOrDefault();
        }
    }
}