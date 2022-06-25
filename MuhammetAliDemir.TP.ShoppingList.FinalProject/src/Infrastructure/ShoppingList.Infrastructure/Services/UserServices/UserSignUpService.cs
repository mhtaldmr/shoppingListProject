using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Interfaces.Services.UserServices;
using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Infrastructure.Services.UserServices
{
    public class UserSignUpService : IUserSignUpService
    {
        private readonly UserManager<User> _userManager;

        public UserSignUpService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task SignUp(UserSignUpViewModel signup)
        {
            var user = await _userManager.FindByEmailAsync(signup.Email);
            if (user is not null)
                throw new Exception("There is a user with this email!");

            var newUser = new User()
            {
                Email = signup.Email,
                PasswordHash = signup.Password,
                UserName = signup.UserName,
                FirstName = signup.FirstName,
                LastName = signup.LastName,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var IsCreated = await _userManager.CreateAsync(newUser, signup.Password);

            if (!IsCreated.Succeeded)
                throw new Exception("User couldnt created!");

        }
    }
}
