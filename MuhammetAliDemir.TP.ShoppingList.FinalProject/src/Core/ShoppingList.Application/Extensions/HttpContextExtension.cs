using Microsoft.AspNetCore.Http;

namespace ShoppingList.Application.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if(httpContext.User is null)
                return string.Empty;

            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}
