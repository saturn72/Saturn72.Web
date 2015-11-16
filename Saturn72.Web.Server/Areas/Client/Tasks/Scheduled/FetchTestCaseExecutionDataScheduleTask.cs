using Automation.Core.Infrastructure.Tasks;
using Automation.Core.Services.Tasks;

namespace Automation.Web.UI.Areas.Client.Tasks.Scheduled
{
    public class FetchTestCaseExecutionDataScheduleTask : IAutoAssignedScheduleTask
    {
        public int Seconds
        {
            get { return 5; }
        }

        public bool StopOnError
        {
            get { return false; }
        }

        public string Name
        {
            get { return "FetchTestCaseExecutionData"; }
        }

        public ITask Task
        {
            get { return new FetchTestCaseExecutionDataTask(); }
        }
    }
}