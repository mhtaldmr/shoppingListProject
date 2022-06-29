using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
            services.AddScoped<IUomRepository, UomRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
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
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ShoppingListDbContext>()
                    .AddDefaultTokenProviders();



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

            //Adding the authorization for role based.
            services.AddAuthorization();





            //Swagger Authorization
            services.AddSwaggerGen(opt =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                opt.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    });
            });


            //Json cycle ignore
            services.AddControllersWithViews().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            return services;
        }
    }
}
