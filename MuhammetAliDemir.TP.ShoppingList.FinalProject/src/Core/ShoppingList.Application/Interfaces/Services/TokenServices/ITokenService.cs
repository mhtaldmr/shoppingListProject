using ShoppingList.Application.ViewModels.Response.TokenResponse;
using System.Security.Claims;

namespace ShoppingList.Application.Interfaces.Services.TokenServices
{
    public interface ITokenService
    {
        TokenResponse GetToken(List<Claim> claims);
    }
}