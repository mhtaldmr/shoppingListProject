using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ShoppingList.Client.Data.User;

namespace ShoppingList.Client.Services.User
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpclient;
        private readonly ProtectedLocalStorage _storage;

        public UserService(HttpClient httpclient, ProtectedLocalStorage storage)
        {
            _httpclient = httpclient;
            _storage = storage;
        }

        public Task<string> Login(UserLogIn logIn)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<string> Signup(UserSignup signup)
        {
            throw new NotImplementedException();
        }
    }
}