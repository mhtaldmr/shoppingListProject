using FluentValidation;
using ShoppingList.Application.ViewModels.Request.ListViewModels;

namespace ShoppingList.Application.Validators.ListValidator
{
    public class ListCreateViewModelValidator : AbstractValidator<ListCreateViewModel>
    {
        public ListCreateViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(1).LessThanOrEqualTo(5).WithMessage("CategoryId must be between 0 and 5");
        }
    }
}
