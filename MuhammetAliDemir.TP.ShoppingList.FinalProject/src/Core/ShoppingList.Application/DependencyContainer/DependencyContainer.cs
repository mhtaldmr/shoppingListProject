using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ShoppingList.Application.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Mapper added to container
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            //Mediatr added to container
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}