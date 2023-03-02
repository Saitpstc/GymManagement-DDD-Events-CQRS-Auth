namespace Shared.Application.CustomValidators;

using FluentValidation;

public class GuidValidator:AbstractValidator<Guid>
{
    public GuidValidator(string Id)
    {
        RuleFor(guid => guid)
            .NotEmpty()
            .WithMessage($"The {Id} must not be empty.")
            .NotEqual(Guid.Empty)
            .WithMessage($"The {Guid.Empty} is not valid")
            .NotNull()
            .WithMessage($"The {Id} must not be null.");
    }
}