using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ShoppingList.Application.Interfaces.DbContext;
using ShoppingList.Application.Interfaces.Repositories;
using ShoppingList.Application.Interfaces.Services.RabbitMq;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.CategoryServices;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.ListServices;
using ShoppingList.Application.Interfaces.Services.RepositoryServices.UomServices;
using ShoppingList.Application.Interfaces.Services.TokenServices;
using ShoppingList.Application.Interfaces.Services.UserServices;
using ShoppingList.Application.Interfaces.UnitOfWork;
using ShoppingList.Domain.Common;
using ShoppingList.Domain.Entities;
using ShoppingList.Infrastructure.Persistence.DbContext;
using ShoppingList.Infrastructure.Repositories;
using ShoppingList.Infrastructure.Services.RabbitMq;
using ShoppingList.Infrastructure.Services.RepositoryServices.CategoryServices;
using ShoppingList.Infrastructure.Services.RepositoryServices.ListServices;
using ShoppingList.Infrastructure.Services.RepositoryServices.UomServices;
using ShoppingList.Infrastructure.Services.TokenServices;
using ShoppingList.Infrastructure.Services.UserServices;
using ShoppingList.Infrastructure.UnitOfWorks;
using System;
using System.Text;

namespace ShoppingList.Infrastructure.DependencyContainer
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            //MSSQL DB configuration
            //var connectionStringMssql = configuration.GetConnectionString("MssqlDefault");
            //services.AddDbContext<ShoppingListDbContext>(options => options.UseSqlServer(connectionStringMssql));

            //Postgre DB Configuration
            var connectionStringPostgre = configuration.GetConnectionString("PostgreDefault");
            services.AddDbContext<ShoppingListDbContext>(options => options.UseNpgsql(connectionStringPostgre));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);


            //MongoDB configuration
            services.AddSingleton<IMongoDbService, MongoDbService>();
            services.Configure<MongoDbSettings>(
                configuration.GetSection("ShoppingListMongoDb"));

            //Application interfaces
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IListRepository, ListRepository>();
            services.AddScoped<IUomRepository, UomRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //Identity
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserSignUpService, UserSignUpService>();
            services.AddScoped<IUserLogInService, UserLogInService>();
            //Jwt token
            services.AddScoped<ITokenService, TokenService>();
            //RabbitMq
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IRabbitMqConnection, RabbitMqConnection>();
            //List services
            services.AddScoped<IListCreateService, ListCreateService>();
            services.AddScoped<IListDeleteService, ListDeleteService>();
            services.AddScoped<IListUpdateService, ListUpdateService>();
            services.AddScoped<IListPatchService, ListPatchService>();
            services.AddScoped<IListGetAllService, ListGetAllService>();
            services.AddScoped<IListGetByIdService, ListGetByIdService>();
            services.AddScoped<IListGetByFilterService, ListGetByFilterService>();
            //Category and Uom services
            services.AddScoped<ICategoryGetService, CategoryGetService>();
            services.AddScoped<IUomGetService, UomGetService>();



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
