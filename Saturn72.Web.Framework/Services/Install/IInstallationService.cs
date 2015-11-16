using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Saturn72.Core.Data;
using Saturn72.Core.Domain.Localization;
using Saturn72.Core.Events;
using Saturn72.Core.Services.Events;
using Saturn72.Extensions;

namespace Saturn72.Web.Framework.Services.Install
{
    public interface IInstallationService
    {
        void InstallAllResources(string resourceFolder);
        void InstallLanguageResource(string resourceFolder);
    }

    public class InstallationService : IInstallationService
    {
        private readonly IRepository<LocaleStringResource> _localeStringResourceRepository;
        public string LanguageResourcesXml = "LanguageResources.xml";
        public string LocalResourceElementName = "LanguageResource";
        private readonly IEventPublisher _eventPublisher;

        public InstallationService(IRepository<LocaleStringResource> localeStringResourceRepository, IEventPublisher eventPublisher)
        {
            _localeStringResourceRepository = localeStringResourceRepository;
            _eventPublisher = eventPublisher;
        }

        public void InstallAllResources(string resourceFolder)
        {
            InstallLanguageResource(resourceFolder);
        }

        public virtual void InstallLanguageResource(string resourceFolder)
        {
            var languageResources =
                XDocument.Load(Path.Combine(resourceFolder, LanguageResourcesXml)).Descendants(LocalResourceElementName);

            languageResources.ForEachItem(lsr =>
            {
                var localeStringResource = new LocaleStringResource
                {
                    ResourceName = lsr.Attribute("Name").Value,
                    ResourceValue = lsr.Attribute("Value").Value
                };
                AddOrUpdateLocaleStringResource(localeStringResource);
            });
        }

        protected virtual void AddOrUpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {

            var query = _localeStringResourceRepository.Table.Where(
                l => l.ResourceName.Equals(localeStringResource.ResourceName, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.Id);
                var lsr = query.FirstOrDefault();
            if(lsr.IsNull())
                _localeStringResourceRepository.Insert(localeStringResource);
            else
            {
                lsr.ResourceValue = localeStringResource.ResourceValue;
                _localeStringResourceRepository.Update(lsr);
                _eventPublisher.EntityUpdated(lsr);
            }
        }
    }
}