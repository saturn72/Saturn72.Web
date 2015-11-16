namespace Saturn72.Web.Framework.WebApi
{
    public interface IWebApiConfiguration
    {
        int Order { get; }
        
        string Name { get; }
        string RouteTemplate { get; }
        object Defaults { get; }
        object Constraints { get; }
    }
}