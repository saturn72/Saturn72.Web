using Automation.Core.Domain.Client;

namespace Automation.Web.UI.Areas.Client.Api
{
    public interface IAutomationRestClient
    {
        ClientMachineExecutionData GetClientMachineExecutionData(int clientId);
    }
}