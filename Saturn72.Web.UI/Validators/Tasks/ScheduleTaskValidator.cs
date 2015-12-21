using FluentValidation;
using Saturn72.Core.Domain.Tasks;
using Saturn72.Core.Services.Localization;
using Saturn72.Web.Framework.Validators;

namespace Saturn72.Web.UI.Validators.Tasks
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