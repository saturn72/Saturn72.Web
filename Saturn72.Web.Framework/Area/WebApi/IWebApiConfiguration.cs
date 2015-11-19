namespace Saturn72.Web.Framework.Area.WebApi
{
    public interface IWebApiConfiguration
    {
        int ConfigureOrder { get; }
        string Name { get; }
        string RouteTemplate { get; }
        object Defaults { get; }
        object Constraints { get; }
    }
}