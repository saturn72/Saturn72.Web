using Automation.Core.Domain.Client;
using Automation.Core.Infrastructure;
using Automation.Core.Infrastructure.Tasks;
using Automation.Core.Services.Execution;
using Automation.Extensions;
using Automation.Web.UI.Areas.Client.Api;

namespace Automation.Web.UI.Areas.Client.Tasks.Scheduled
{
    public class FetchTestCaseExecutionDataTask : ITask
    {
        private static readonly ClientSettings ClientSettings = EngineContext.Current.Resolve<ClientSettings>();

        public int Order
        {
            get { return 100; }
        }

        public void Execute()
        {
            var autoRestClient = EngineContext.Current.Resolve<IAutomationRestClient>();

            var clientMachineExecutionData =
                autoRestClient.GetClientMachineExecutionData(ClientSettings.ClientId);
            if (clientMachineExecutionData.IsNull() || clientMachineExecutionData.TestCaseExecutionDatas.IsEmpty())
                return;

            var executionService = EngineContext.Current.Resolve<ITestCaseExecutionDataService>();
            clientMachineExecutionData.TestCaseExecutionDatas.ForEachItem(t =>
            {
                if (executionService.GetTestCaseExecutionDataById(t.Id).IsNull())
                    executionService.Execute(t);
            });
        }
    }
}