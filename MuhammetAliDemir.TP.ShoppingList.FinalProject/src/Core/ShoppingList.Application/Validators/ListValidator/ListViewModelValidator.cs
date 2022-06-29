using FluentValidation;
using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Application.Validators.ListValidator
{
    public class ListViewModelValidator : AbstractValidator<ListViewModel> 
    {
        public ListViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(1).WithMessage("CategoryId must be between 0 and 5");
        }
    }
}