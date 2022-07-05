using NLog.Web;
using ShoppingList.Application.DependencyContainer;
using ShoppingList.Infrastructure.DependencyContainer;
using ShoppingList.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// NLog: Setup NLog for Dependency injection
// if needed, other logger systems can be eleminated by uncommenting line below!
//builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

//Add Authentication for users.
app.UseAuthentication();
app.UseAuthorization();

//my custom global exception
app.UseCustomeGlobalException();

app.MapControllers();

app.Run();


//for using the test project..
public partial class Program { }