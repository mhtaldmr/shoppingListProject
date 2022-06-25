using ShoppingList.Application.ViewModels.Response.TokenViewModels;
using System.Security.Claims;

namespace ShoppingList.Application.Interfaces.Services.TokenServices
{
    public interface ITokenService
    {
        TokenResponseViewModel GetToken(List<Claim> claims);
    }
}
