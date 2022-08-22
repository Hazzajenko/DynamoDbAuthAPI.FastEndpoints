using DynamoDbAuthAPI.Contracts.Requests;
using FluentValidation;

namespace DynamoDbAuthAPI.Validation;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is invalid")
            .MinimumLength(3)
            .WithMessage("Email has to be a minimum of 3 characters");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(5)
            .WithMessage("Password needs a minimum of 5 characters")
            .MaximumLength(12)
            .WithMessage("Password has to be a maximum of 12 characters");
    }
}