using FluentValidation;
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

            //DistributedCaching //Redis
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = "localhost:6379";
                opt.InstanceName = "RedisCacheServer";
            });

            //FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}