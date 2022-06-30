using ShoppingList.Server.Exceptions;

namespace ShoppingList.Server.Extensions
{
    public static class CustomGlobalExceptionExtension
    {
        public static IApplicationBuilder UseCustomeGlobalException(this IApplicationBuilder builder)
            => builder.UseMiddleware<CustomGlobalException>();
    }
}
