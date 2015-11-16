using FluentValidation;

namespace Saturn72.Web.Framework.Validators
{
    public abstract class BaseSaturn72Validator<T> : AbstractValidator<T> where T : class
    {
        protected BaseSaturn72Validator()
        {
            PostInitialize();
        }

        /// <summary>
        ///     Developers can override this method in custom partial classes
        ///     in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {
        }
    }
}