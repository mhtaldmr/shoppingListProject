using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.TokenServices;
using ShoppingList.Application.Interfaces.Services.UserServices;
using ShoppingList.Application.Interfaces.UnitOfWork;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;
using ShoppingList.Infrastructure.Repositories;
using ShoppingList.Infrastructure.Services.TokenServices;
using ShoppingList.Infrastructure.Services.UserServices;
using ShoppingList.Infrastructure.UnitOfWorks;
using System.Text;

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
            services.AddScoped<IUserSignUpService, UserSignUpService>();
            services.AddScoped<IUserLogInService, UserLogInService>();
            services.AddScoped<ITokenService, TokenService>();

            //Identity configurations
            services.AddIdentity<User, IdentityRole>(opt =>
                        {
                            opt.User.RequireUniqueEmail = true;
                            opt.Password.RequiredLength = 8;
                            opt.Lockout.AllowedForNewUsers = true;
                            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                            opt.Lockout.MaxFailedAccessAttempts = 3;
                        })
                    .AddEntityFrameworkStores<ShoppingListDbContext>();



            //TokenValidationParameter Object
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = configuration["JWT:Audience"],
                ValidIssuer = configuration["JWT:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),
                ClockSkew = TimeSpan.Zero
            };

            //JWT Bearer configurations
            services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                    .AddJwtBearer(opt => opt.TokenValidationParameters = tokenValidationParameters);




            return services;
        }
    }
}
