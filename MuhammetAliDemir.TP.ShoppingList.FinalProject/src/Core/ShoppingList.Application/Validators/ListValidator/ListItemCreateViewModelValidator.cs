using FluentValidation;
using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Application.Validators.ListValidator
{
    public class ListItemCreateViewModelValidator : AbstractValidator<ListItemCreateViewModel>
    {
        public ListItemCreateViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Please specify a Name!");
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity can not be less then 0!");
            RuleFor(x => x.UoMId).GreaterThanOrEqualTo(1).WithMessage("UoMId must be between 0 and 5");
        }
    }
}
