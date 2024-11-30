using EStockFlow.Models;
using EStockFlow.Repositories;
using FluentValidation;

namespace EStockFlow.Validators
{
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionRequest>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTransactionValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Item)
                .NotEmpty()
                .NotEqual(Guid.Empty)
                .Must(IsProductExists)
                .OverridePropertyName("item")
                .WithName("Item");

            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThan(0)
                .Must(IsSufficientStock)
                .OverridePropertyName("quantity")
                .WithName("Quantity");

            RuleFor(x => x.Category)
                .NotNull()
                .IsInEnum()
                .OverridePropertyName("category")
                .WithName("Category");

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .OverridePropertyName("price")
                .WithName("Price");
        }

        private bool IsProductExists(Guid id)
        {
            return _unitOfWork.ProductRepository.IsProductExists(id);
        }

        private bool IsSufficientStock(CreateTransactionRequest request, int quantity)
        {
            return _unitOfWork.ProductRepository.IsSufficientStock(quantity, request.Item);
        }
    }
}