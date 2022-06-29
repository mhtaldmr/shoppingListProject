using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;

namespace ShoppingList.Application.Interfaces.Services.UserServices
{
    public interface IUserSignUpService
    {
        Task<Result<UserSignUpViewModel>> SignUp(UserSignUpViewModel signup);
    }
}
