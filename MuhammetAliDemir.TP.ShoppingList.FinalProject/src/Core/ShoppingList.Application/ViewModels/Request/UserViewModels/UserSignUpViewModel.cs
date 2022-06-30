using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Application.ViewModels.Request.UserViewModels
{
    public class UserSignUpViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}