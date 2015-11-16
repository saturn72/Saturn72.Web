using Automation.Core.Domain.Tasks;
using Automation.Core.Services.Localization;
using Automation.Web.Framework.Validators;
using FluentValidation;

namespace Automation.Web.UI.Validators.Tasks
{
    public class ScheduleTaskValidator : BaseSaturn72Validator<ScheduleTask>
    {
        public ScheduleTaskValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Admin.System.ScheduleTasks.Fields.Messages.Name.Required"));
            RuleFor(x => x.Seconds)
                .GreaterThan(0)
                .WithMessage(localizationService.GetResource("Admin.System.ScheduleTasks.Fields.Messages.Seconds.Positive"));
        }
    }
}