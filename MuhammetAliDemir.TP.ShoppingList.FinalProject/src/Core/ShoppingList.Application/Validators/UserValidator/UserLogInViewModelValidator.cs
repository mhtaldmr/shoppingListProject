using FluentValidation;
using ShoppingList.Application.ViewModels.Request.UserViewModels;

namespace ShoppingList.Application.Validators.UserValidator
{
    public class UserLogInViewModelValidator : AbstractValidator<UserLogInViewModel>
    {
        public UserLogInViewModelValidator()
        {
            RuleFor(x => x.Email).Must(x => !string.IsNullOrWhiteSpace(x)).NotEmpty().WithMessage("Email field can not be empty!");
            RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Email must be valid!");
            RuleFor(x => x.Password).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Password field can not be empty");
        }
    }
}