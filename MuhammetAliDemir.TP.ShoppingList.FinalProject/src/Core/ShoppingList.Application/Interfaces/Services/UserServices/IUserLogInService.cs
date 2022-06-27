using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Application.ViewModels.Response.MainResponse;
using ShoppingList.Application.ViewModels.Response.TokenResponse;

namespace ShoppingList.Application.Interfaces.Services.UserServices
{
    public interface IUserLogInService
    {
        Task<Result<TokenResponse>> LogIn(UserLogInViewModel login);
    }
}
