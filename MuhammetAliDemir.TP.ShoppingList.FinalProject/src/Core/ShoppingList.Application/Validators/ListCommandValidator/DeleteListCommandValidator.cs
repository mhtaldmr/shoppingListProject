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
                .NotEqual(0).WithMessage("ListId can not be Zero!");
        }
    }
}