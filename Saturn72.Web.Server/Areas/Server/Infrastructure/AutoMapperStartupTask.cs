using Automation.Core.Domain.Client;
using Automation.Core.Domain.Job;
using Automation.Core.Infrastructure.Tasks;
using Automation.Web.UI.Areas.Server.Models.Job;
using Automation.Web.UI.Areas.Server.Models.JobPlan;
using Automation.Web.UI.Models.ClientMachine;
using AutoMapper;

namespace Automation.Web.UI.Areas.Server.Infrastructure
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public int Order
        {
            get { return 100; }
        }

        public void Execute()
        {
            Mapper.CreateMap<AutomationJob, AutomationJobModel>();
            Mapper.CreateMap<AutomationJobModel, AutomationJob>();

            Mapper.CreateMap<AutomationJobPlan, AutomationJobPlanModel>()
                .ForMember(dest => dest.AutomationJob, mo => mo.MapFrom(src => src.Name));

            Mapper.CreateMap<AutomationJobPlanModel, AutomationJobPlan>()
                .ForMember(dest => dest.AutomationJob, mo => mo.Ignore());

            //clientMachine
            Mapper.CreateMap<ClientMachineModel, ClientMachine>();
        }
    }
}