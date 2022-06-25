using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.Infrastructure.Persistence.DbContext;

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
        

            return services;
        }
    }
}
