using Demo.Business.Contracts;
using Demo.Business.Notifications;
using Demo.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace Demo.Business.Services.Base
{
    public abstract class BaseServices
    {
        private readonly INotifier _notifier;

        public BaseServices(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected virtual void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notify(error.ErrorMessage);
        }

        protected virtual void Notify(string message)
            => _notifier.Handle(new Notification(message));

        protected virtual bool ExecuteValidation<TV, TE>(TV validation, TE entity) 
            where TV : AbstractValidator<TE> where TE : EntityBase
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }
    }
}
