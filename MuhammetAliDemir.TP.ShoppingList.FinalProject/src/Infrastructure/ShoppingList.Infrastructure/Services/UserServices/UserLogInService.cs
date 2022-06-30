using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Interfaces.Services.TokenServices;
using ShoppingList.Application.Interfaces.Services.UserServices;
using ShoppingList.Application.Validators.UserValidator;
using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.TokenResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Infrastructure.Services.UserServices
{
    public class UserLogInService : IUserLogInService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public UserLogInService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<Result<TokenResponse>> LogIn(UserLogInViewModel login)
        {
            var validator = new UserLogInViewModelValidator();
            await validator.ValidateAsync(login);

            var existingUser = await _userManager.FindByEmailAsync(login.Email);

            if (existingUser == null)
                throw new InvalidOperationException("User does not exist!");
            if (await _userManager.IsLockedOutAsync(existingUser))
                throw new InvalidOperationException("User is Locked");

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, login.Password);


            if (!isCorrect)
            { 
                await AccessRightControl(existingUser);
                throw new InvalidOperationException("Password is not correct");
            }

            if (existingUser.AccessFailedCount < 3)
            {
                existingUser.AccessFailedCount = 0;
                await _userManager.UpdateAsync(existingUser);
            }

            await _signInManager.CheckPasswordSignInAsync(existingUser, login.Password, false);
            await _signInManager.SignInAsync(existingUser, false);

            //getting the token with roles and user
            var roles = await _userManager.GetRolesAsync(existingUser);
            var jwtToken = _tokenService.GetToken(existingUser, roles);

            return Result.Success(jwtToken, "Token is generated!.");
        }


        private async Task AccessRightControl(User existingUser)
        {
            existingUser.AccessFailedCount++;
            await _userManager.UpdateAsync(existingUser);

            if (existingUser.AccessFailedCount >= 3)
            {
                existingUser.LockoutEnabled = true;
                await _userManager.UpdateAsync(existingUser);
                throw new InvalidOperationException("User blocked");
            }
        }
    }
}