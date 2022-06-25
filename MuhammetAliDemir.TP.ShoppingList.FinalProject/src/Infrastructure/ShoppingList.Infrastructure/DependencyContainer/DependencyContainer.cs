using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.UnitOfWork;
using ShoppingList.Infrastructure.Persistence.DbContext;
using ShoppingList.Infrastructure.Repositories;
using ShoppingList.Infrastructure.UnitOfWorks;

namespace ShoppingList.Infrastructure.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext configuration
            var connectionString = configuration.GetConnectionString("Default");
            services.AddDbContext<ShoppingListDbContext>(options => options.UseSqlServer(connectionString));


            //Interface contracts
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
