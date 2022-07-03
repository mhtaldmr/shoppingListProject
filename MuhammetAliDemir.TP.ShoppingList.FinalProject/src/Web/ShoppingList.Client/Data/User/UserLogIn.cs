using System.ComponentModel.DataAnnotations;

namespace ShoppingList.Client.Data.User
{
    public class UserLogIn
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
