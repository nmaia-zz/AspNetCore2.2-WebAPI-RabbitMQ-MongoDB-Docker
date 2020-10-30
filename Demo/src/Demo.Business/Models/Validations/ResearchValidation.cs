using Demo.Domain.Entities;
using FluentValidation;

namespace Demo.Business.Models.Validations
{
    public class ResearchValidation : AbstractValidator<Research>
    {
        public ResearchValidation()
        {
            RuleFor(r => r.Region)
                .NotEmpty().WithMessage("The field {PropertyName} is required");
        }
    }
}
