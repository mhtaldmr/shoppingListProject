using FluentValidation;
using ShoppingList.Application.Features.ListFeatures.Commands.Create;

namespace ShoppingList.Application.Validators.ListCommandValidator
{
    public class CreateListCommandValidator : AbstractValidator<CreateListCommand>
    {
        public CreateListCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotNull().NotEmpty()
                .WithMessage("Please specify a Title!");
            RuleFor(x => x.Description)
                .NotNull().NotEmpty()
                .WithMessage("Please specify a Title!");
            RuleFor(x => x.CategoryId)
                .GreaterThanOrEqualTo(1).WithMessage("CategoryId must be a valid key!")
                .LessThanOrEqualTo(5).WithMessage("CategoryId must be a valid key!");

            //For items inside the lists //Nested objects validation
            RuleForEach(x => x.Items)
                .ChildRules(items =>
                    items.RuleFor(x => x.UoMId)
                        .NotEmpty().WithMessage("Please specify a valid Id for Unit!")
                        .NotNull().WithMessage("UomId can not be null!")
                        .NotEqual(0).WithMessage("UomId can not be Zero!"))
                .ChildRules(items =>
                    items.RuleFor(x => x.Name)
                        .NotEmpty().WithMessage("Please specify a valid Id for ItemName!")
                        .NotNull().WithMessage("ItemName can not be null!"))
                .ChildRules(items =>
                    items.RuleFor(x => x.Quantity)
                        .GreaterThanOrEqualTo(0).WithMessage("Quantity can not be less then 0!"));
        }
    }
}