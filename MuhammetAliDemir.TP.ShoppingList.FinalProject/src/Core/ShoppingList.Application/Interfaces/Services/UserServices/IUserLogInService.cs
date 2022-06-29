using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Application.ViewModels.Response.BaseResponses;
using ShoppingList.Application.ViewModels.Response.TokenResponses;

namespace ShoppingList.Application.Interfaces.Services.UserServices
{
    public interface IUserLogInService
    {
        Task<Result<TokenResponse>> LogIn(UserLogInViewModel login);
    }
}
