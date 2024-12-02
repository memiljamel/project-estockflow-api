using EStockFlow.Models;
using EStockFlow.Repositories;
using FluentValidation;

namespace EStockFlow.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .OverridePropertyName("name")
                .WithName("Name");

            RuleFor(x => x.Username)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .Must(IsUsernameUnique)
                .OverridePropertyName("username")
                .WithName("Username");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100)
                .OverridePropertyName("password")
                .WithName("Password");

            RuleFor(x => x.PasswordConfirmation)
                .Equal(x => x.Password)
                .OverridePropertyName("passwordConfirmation")
                .WithName("Password Confirmation");
        }

        private bool IsUsernameUnique(string username)
        {
            return _unitOfWork.UserRepository.IsUsernameUnique(username);
        }
    }
}