using ShoppingList.Client.Data.User;
using ShoppingList.Client.Services.Category;
using ShoppingList.Client.Services.List;
using ShoppingList.Client.Services.UnitOfMaterials;

var builder = WebApplication.CreateBuilder(args);

//base url
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5070") });

//Service registirations
builder.Services.AddHttpClient();
builder.Services.AddSingleton<UserLoginData>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUomService, UomService>();
builder.Services.AddScoped<IListService, ListService>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
