using Automation.Core.Domain.Client;
using Automation.Core.Domain.Job;
using Automation.Web.UI.Areas.Server.Models.Job;
using Automation.Web.UI.Areas.Server.Models.JobPlan;
using Automation.Web.UI.Models.ClientMachine;
using Automation.Web.Framework.Extensions;

namespace Automation.Web.UI.Areas.Server.Infrastructure
{
    public static class MappingExtensions
    {
        //AutomationJob
        public static AutomationJobModel ToModel(this AutomationJob entity)
        {
            return entity.ToModel<AutomationJob, AutomationJobModel>();
        }

        //AutomationJob
        public static AutomationJob ToEntity(this AutomationJobModel model)
        {
            return model.ToEntity<AutomationJobModel, AutomationJob>();
        }

        //AutomationJobPlan
        public static AutomationJobPlanModel ToModel(this AutomationJobPlan entity)
        {
            return entity.ToModel<AutomationJobPlan, AutomationJobPlanModel>();
        }
        public static AutomationJobPlan ToEntity(this AutomationJobPlanModel model)
        {
            return model.ToEntity<AutomationJobPlanModel, AutomationJobPlan>();
        }

        //clientMachine
        public static ClientMachine ToEntity(this ClientMachineModel model)
        {
            return model.ToEntity<ClientMachineModel, ClientMachine>();
        }
    }
}