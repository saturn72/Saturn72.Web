using Automation.Core.Infrastructure;
using Automation.Core.Infrastructure.Tasks;
using Automation.Core.Services.Execution;
using Automation.Core.Services.Tasks;

namespace Automation.Web.UI.Areas.Client.Tasks.Scheduled
{
    public class ExecuteTestCaseSchduleTask:IAutoAssignedScheduleTask
    {
        public int Seconds
        {
            get { return 5; }
        }

        public bool StopOnError {
            get { return false; }
        }
        public string Name { get { return "ExecuteTestCaseSchduleTask"; } }
        public ITask Task { get{return new ExecuteTestCaseTask();} }
    }

    public class ExecuteTestCaseTask : ITask
    {
        private static readonly ITestExecutor TestExecutor = EngineContext.Current.Resolve<ITestExecutor>();

        public int Order { get { return 1000; } }
        
        public void Execute()
        {
            //TestExecutor.ExecuteNext();
        }
    }
}