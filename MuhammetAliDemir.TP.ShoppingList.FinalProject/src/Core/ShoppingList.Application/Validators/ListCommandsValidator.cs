using FluentValidation;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;

namespace ShoppingList.Application.Validators
{
    internal class ListCommandsValidator : AbstractValidator<CreateListCommand>
    {
        public ListCommandsValidator()
        {
            
        }
    }
}
