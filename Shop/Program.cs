//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI;
//using Microsoft.EntityFrameworkCore;
//using Shop.Application.Services;
//using Shop.Areas.Identity;
//using Shop.Domain.Entities;
//using Shop.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Data;
using Shop.Infrastructure.Data.Seeding;

var builder = WebApplication.CreateBuilder(args);

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var appSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();

//builder.Services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Shop.Infrastructure")));

Shop.Application.DependencyResolver.DependencyResolverService.Register(builder.Services);
Shop.Infrastructure.DependencyResolver.DependencyResolverService.Register(builder.Services, appSettings);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddTransient<IContextSeed,ContextSeed>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    Shop.Infrastructure.DependencyResolver.DependencyResolverService.MigrateDatabase(scope.ServiceProvider);
}

app.Run();
