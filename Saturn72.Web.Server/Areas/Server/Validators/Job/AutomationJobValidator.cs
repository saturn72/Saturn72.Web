using Automation.Core.Services.Localization;
using Automation.Web.Framework.Validators;
using Automation.Web.UI.Areas.Server.Models.Job;
using FluentValidation;

namespace Automation.Web.UI.Areas.Server.Validators.Job
{
    public class AutomationJobValidator : BaseSaturn72Validator<AutomationJobModel>
    {
        public AutomationJobValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.DisplayName)
                .NotEmpty()
                .WithMessage(
                    localizationService.GetResource("Automation.AutomationJob.Fields.DisplayName.Messages.NotEmpty"));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Automation.AutomationJob.Fields.Name.Messages.NotEmpty"));

        }
    }
}