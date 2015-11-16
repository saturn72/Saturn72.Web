using Autofac;
using Automation.Core.Infrastructure;
using Automation.Core.Infrastructure.DependencyManagement;
using Automation.Core.Net.Rest;
using Automation.Web.UI.Areas.Client.Api;

namespace Automation.Web.UI.Areas.Client.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get { return 100; }
        }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //Rest clients
            //TODO: load from settings file
          
            var autoServerUrl = "localhost:54490/api";

            builder.Register(c => new CommonRestClient(autoServerUrl))
                .As<IRestClient>()
                .SingleInstance();
            
            builder.RegisterType<AutomationRestClient>().As<IAutomationRestClient>()
                .InstancePerLifetimeScope();
        }
    }
}