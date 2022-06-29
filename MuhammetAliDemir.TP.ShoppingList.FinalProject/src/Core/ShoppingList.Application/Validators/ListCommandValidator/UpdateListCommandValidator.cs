using FluentValidation;
using ShoppingList.Application.Features.ListFeatures.Commands.Update;

namespace ShoppingList.Application.Validators.ListCommandValidator
{
    public class UpdateListCommandValidator : AbstractValidator<UpdateListCommand>
    {
        public UpdateListCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(1).WithMessage("CategoryId must be between 0 and 5");
        }
    }
}
