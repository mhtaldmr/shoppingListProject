using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Application.ViewModels.Request.UserViewModels
{
    public class UserLogInViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}