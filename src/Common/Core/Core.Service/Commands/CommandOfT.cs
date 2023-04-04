using FluentValidation;
using MediatR;

namespace Core.Service.Commands
{
    public abstract class Command<T> : CommandBase, IRequest<T>
    {
    }

    public abstract class Command<T, TValidator> : Command<T>
        where TValidator : IValidator, new()
    {
        public override bool IsValid()
        {
            var validator = new TValidator();
            var ctx = new ValidationContext<Command<T>>(this);
            ValidationResult = validator.Validate(ctx);
            return ValidationResult.IsValid;
        }
    }
}
