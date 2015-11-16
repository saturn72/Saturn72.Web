using System.ComponentModel;
using Saturn72.Core.Infrastructure;
using Saturn72.Core.Services.Localization;

namespace Saturn72.Web.Framework
{
    public class Saturn72ResourceDisplayNameAttribute : DisplayNameAttribute, IModelAttribute
    {
        public Saturn72ResourceDisplayNameAttribute(string resourceKey)
            : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey { get; set; }

        public override string DisplayName
        {
            get
            {
                return EngineContext.Current
                    .Resolve<ILocalizationService>()
                    .GetResource(ResourceKey, true, ResourceKey);
            }
        }

        public string Name
        {
            get { return "Saturn72ResourceDisplayName"; }
        }
    }
}