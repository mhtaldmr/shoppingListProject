using FluentValidation;
using ShoppingList.Application.Features.ListFeatures.Commands.Delete;

namespace ShoppingList.Application.Validators.ListCommandValidator
{
    public class DeleteListCommandValidator : AbstractValidator<DeleteListCommand>
    {
        public DeleteListCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Please specify a valid Id for List!")
                .NotNull().WithMessage("ListId can not be null!")
                .GreaterThanOrEqualTo(1).WithMessage("ListId must be valid!");
        }
    }
}