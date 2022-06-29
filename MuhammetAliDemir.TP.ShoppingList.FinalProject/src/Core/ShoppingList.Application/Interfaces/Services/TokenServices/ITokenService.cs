using ShoppingList.Application.ViewModels.Response.TokenResponses;
using System.Security.Claims;

namespace ShoppingList.Application.Interfaces.Services.TokenServices
{
    public interface ITokenService
    {
        TokenResponse GetToken(List<Claim> claims);
    }
}