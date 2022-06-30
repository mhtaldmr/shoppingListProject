using FluentValidation;
using ShoppingList.Application.ViewModels.Request.UserViewModels;

namespace ShoppingList.Application.Validators.UserValidator
{
    public class UserSignUpViewModelValidator : AbstractValidator<UserSignUpViewModel>
    {
        public UserSignUpViewModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserNamee field can not be empty!");
            RuleFor(x => x.Email).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Email field can not be empty!");
            RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("Email must be valid!");
            RuleFor(x => x.Password).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Password field can not be empty!");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Password can not be less then 8 characters!");
            RuleFor(x => x.FirstName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("First Name field can not be empty!");
            RuleFor(x => x.LastName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Last Name field can not be empty!");
            RuleFor(x => x.FirstName).MaximumLength(100).WithMessage("Firt Name can not be more then 100 characters!");
            RuleFor(x => x.LastName).MaximumLength(100).WithMessage("Last Name can not be more then 100 characters!");
        }
    }
}