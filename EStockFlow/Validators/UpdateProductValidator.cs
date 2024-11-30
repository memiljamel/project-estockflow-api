using EStockFlow.Entities;
using EStockFlow.Models;
using FluentValidation;

namespace EStockFlow.Validators
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .OverridePropertyName("name")
                .WithName("Name");
            
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0)
                .OverridePropertyName("price")
                .WithName("Price");
            
            RuleFor(x => x.InitialStock)
                .NotEmpty()
                .GreaterThan(0)
                .OverridePropertyName("initialStock")
                .WithName("Initial Stock");
            
            RuleFor(x => x.Category)
                .NotNull()
                .IsInEnum()
                .OverridePropertyName("category")
                .WithName("Category");

            RuleFor(x => x.Image)
                .SetValidator(new ImageValidator())
                .OverridePropertyName("image")
                .WithName("Image");
        }
    }
}