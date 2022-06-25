using ShoppingList.Application.ViewModels.Request.UserViewModels;

namespace ShoppingList.Application.Interfaces.Services.UserServices
{
    public interface IUserLogInService
    {
        Task LogIn(UserLogInViewModel login);
    }
}
