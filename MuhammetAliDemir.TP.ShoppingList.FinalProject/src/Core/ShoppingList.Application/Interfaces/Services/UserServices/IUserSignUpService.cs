using ShoppingList.Application.ViewModels.Request.UserViewModels;

namespace ShoppingList.Application.Interfaces.Services.UserServices
{
    public interface IUserSignUpService
    {
        Task SignUp(UserSignUpViewModel signup);
    }
}
