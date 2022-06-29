using FluentValidation;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;

namespace ShoppingList.Application.Validators.ListCommandValidator
{
    public class CreateListCommandValidator : AbstractValidator<CreateListCommand>
    {
        public CreateListCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please specify a Title!");
            RuleFor(x => x.Title).NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(1).WithMessage("CategoryId must be between 0 and 5");
        }
    }
}