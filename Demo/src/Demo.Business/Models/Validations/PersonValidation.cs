using Demo.Domain.Entities;
using FluentValidation;

namespace Demo.Business.Models.Validations
{
    public class PersonValidation : AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 100)
                .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characteres");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("The field {PropertyName} is required")
                .Length(2, 100)
                .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} characteres");

            RuleFor(p => p.Gender)
                .NotEmpty().WithMessage("The field {PropertyName} is required");

            RuleFor(p => p.SkinColor)
                .NotEmpty().WithMessage("The field {PropertyName} is required");

            RuleFor(p => p.Filiation)
                .NotEmpty().WithMessage("The field {PropertyName} is required");

            RuleFor(p => p.Schooling)
                .NotEmpty().WithMessage("The field {PropertyName} is required");

        }
    }
}
