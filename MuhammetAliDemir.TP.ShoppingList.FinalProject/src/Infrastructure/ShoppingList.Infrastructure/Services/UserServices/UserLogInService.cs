using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Interfaces.Services.UserServices;
using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Infrastructure.Services.UserServices
{
    public class UserLogInService : IUserLogInService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserLogInService (UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task LogIn(UserLogInViewModel login)
        {
            var existingUser = await _userManager.FindByEmailAsync(login.Email);

            if (existingUser == null)
                throw new Exception("User does not exist!");
            if (await _userManager.IsLockedOutAsync(existingUser))
                throw new Exception("User is Locked");


            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, login.Password);
            await _signInManager.CheckPasswordSignInAsync(existingUser, login.Password, false);

            if (!isCorrect)
            {
                await _userManager.AccessFailedAsync(existingUser);
                throw new Exception("Access Failed");
            }

            await _signInManager.SignInAsync(existingUser, false);


        }
    }
}
