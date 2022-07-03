using ShoppingList.Client.Data.User;

namespace ShoppingList.Client.Services.User
{
    public interface IUserService
    {
        Task<string> Signup(UserSignup signup);
        Task<string> Login(UserLogIn logIn);
        Task Logout();
    }
}