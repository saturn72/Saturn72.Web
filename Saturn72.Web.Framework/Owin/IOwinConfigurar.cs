using Owin;

namespace Saturn72.Web.Framework.Owin
{
    public interface IOwinConfigurar
    {
        void Configure(IAppBuilder appBuilder);
        int Order { get; }
    }
}