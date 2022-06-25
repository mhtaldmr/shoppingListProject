using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ShoppingList.Application.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}