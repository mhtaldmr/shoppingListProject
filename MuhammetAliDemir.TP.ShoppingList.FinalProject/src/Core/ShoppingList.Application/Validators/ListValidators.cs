using FluentValidation;
using ShoppingList.Application.ViewModels.Request.ListViewModels;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Validators
{
    public class ListValidators : AbstractValidator<List>
    {
        public ListValidators()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Please specify a Description!");
            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("Please specify a UserId!");
            RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).WithMessage("CategoryId must be between 0 and 5");
        }
    }

    public class ListViewModelValidators : AbstractValidator<ListViewModel>
    {
        public ListViewModelValidators()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Please specify a Title!");
            RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).WithMessage("CategoryId must be between 0 and 5");
        }
    }

    public class ListItemViewModelValidators : AbstractValidator<ListItemViewModel>
    {
        public ListItemViewModelValidators()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Please specify a Name!");
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity can not be less then 0!");
            RuleFor(x => x.UoMId).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).WithMessage("UoMId must be between 0 and 5");
        }
    }
}
