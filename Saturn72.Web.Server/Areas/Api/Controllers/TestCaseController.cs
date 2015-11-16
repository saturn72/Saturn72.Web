using System.Collections.Generic;
using Automation.Core.Domain.Job;
using Automation.Core.Services.Execution;
using Automation.Web.Framework.Controllers;

namespace Automation.Web.UI.Areas.Api.Controllers
{
    public class TestCaseController : Saturn72ApiControllerBase
    {
        private readonly ITestCaseExecutionDataService _executionService;

        public TestCaseController(ITestCaseExecutionDataService executionService)
        {
            _executionService = executionService;
        }

        // GET: api/TO_DELETE
        public IEnumerable<AutomationJobExecutionData> Get()
        {
            return _executionService.GetAllTestCaseExecutionData();
        }

        // GET: api/ExecutionApi/{clientId} 
        public IEnumerable<AutomationJobExecutionData> Get(int id)
        {
            return _executionService.GetTestCaseExecutionDataByClientId(id);
        }

        //// POST: api/TO_DELETE
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/TO_DELETE/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/TO_DELETE/5
        //public void Delete(int id)
        //{
        //}
    }
}