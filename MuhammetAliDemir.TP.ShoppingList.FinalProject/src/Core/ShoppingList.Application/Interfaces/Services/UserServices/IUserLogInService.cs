using ShoppingList.Application.ViewModels.Request.UserViewModels;
using ShoppingList.Application.ViewModels.Response.MainResponse;
using ShoppingList.Application.ViewModels.Response.TokenViewModels;

namespace ShoppingList.Application.Interfaces.Services.UserServices
{
    public interface IUserLogInService
    {
        Task<Result<TokenResponseViewModel>> LogIn(UserLogInViewModel login);
    }
}
