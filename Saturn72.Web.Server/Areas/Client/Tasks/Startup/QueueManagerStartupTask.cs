using Automation.Core.Infrastructure;
using Automation.Core.Infrastructure.Tasks;
using Automation.Core.Services.Execution;

namespace Automation.Web.UI.Areas.Client.Tasks.Startup
{
    public class QueueManagerStartupTask:IStartupTask
    {
        public int Order { get { return 100; } }

        public void Execute()
        {
            var executionService = EngineContext.Current.Resolve<IExecutionQueueManager>();
            executionService.Reload();
        }
    }
}