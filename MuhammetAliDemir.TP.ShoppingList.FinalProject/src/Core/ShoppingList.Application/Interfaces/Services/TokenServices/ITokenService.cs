using ShoppingList.Application.ViewModels.Response.TokenResponses;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Interfaces.Services.TokenServices
{
    public interface ITokenService
    {
        TokenResponse GetToken(User user, IList<string> roles);
    }
}