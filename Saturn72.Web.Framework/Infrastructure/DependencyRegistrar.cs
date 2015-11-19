using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Saturn72.Core;
using Saturn72.Core.Caching;
using Saturn72.Core.Infrastructure;
using Saturn72.Core.Infrastructure.DependencyManagement;
using Saturn72.Core.Services.Authentication;
using Saturn72.Core.Services.Localization;
using Saturn72.Core.Services.Users;
using Saturn72.Web.Framework.Area.WebApi;
using Saturn72.Web.Framework.Fakes;
using Saturn72.Web.Framework.Routes;
using Saturn72.Web.Framework.Services.Install;
using Saturn72.Web.Framework.UI;

namespace Saturn72.Web.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 100;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HttpContext
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null
                    ? (new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                    : (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();


            builder.RegisterType<PageHeadBuilder>().As<IPageHeadBuilder>().InstancePerLifetimeScope();

            //MVC
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //Api
            builder.RegisterApiControllers(typeFinder.GetAssemblies().ToArray());

            //Route
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            //WebApi
            builder.RegisterType<WebApiConfigurar>().As<IWebApiConfigurar>().SingleInstance();

            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //Cache
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            //WebHelper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerDependency();
            //Services
            builder.RegisterType<LocalizationService>().As<ILocalizationService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<InstallationService>().As<IInstallationService>().InstancePerLifetimeScope();
        }
    }
}