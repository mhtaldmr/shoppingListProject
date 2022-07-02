using Microsoft.AspNetCore.Identity;
using ShoppingList.Application.Interfaces.Services.UserServices;
using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
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
        public async Task<Result<UserSignUpViewModel>> SignUp(UserSignUpViewModel signup)
        {
            var user = await _userManager.FindByEmailAsync(signup.Email);
            if (user is not null)
                return Result.Fail(signup,"There is a user with this email!");
                //throw new Exception("There is a user with this email!");

            var newUser = new User()
            {
                Email = signup.Email,
                PasswordHash = signup.Password,
                UserName = signup.UserName,
                FirstName = signup.FirstName,
                LastName = signup.LastName
            };

            var IsCreated = await _userManager.CreateAsync(newUser, signup.Password);
            await _userManager.AddToRoleAsync(newUser, "user");

            if (!IsCreated.Succeeded)
                return Result.Fail(signup, IsCreated.Errors.Select(x => x.Description).FirstOrDefault());
                //throw new Exception("User couldnt created!");
            return Result.Success(signup, "Successful!");
        }
    }
}