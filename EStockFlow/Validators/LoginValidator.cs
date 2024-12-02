using EStockFlow.Models;
using FluentValidation;

namespace EStockFlow.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .OverridePropertyName("username")
                .WithName("Username");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100)
                .OverridePropertyName("password")
                .WithName("Password");
        }
    }
}