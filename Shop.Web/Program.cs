var builder = WebApplication.CreateBuilder(args);

var appSettings = new ConfigurationBuilder()//
    .SetBasePath(Directory.GetCurrentDirectory())//
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)//
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)//
    .Build();//

Shop.Application.DependencyResolver.DependencyResolverService.Register(builder.Services);//
Shop.Infrastructure.DependencyResolver.DependencyResolverService.Register(builder.Services, appSettings);//
builder.Services.AddRazorPages();//
builder.Services.AddServerSideBlazor();//
builder.Services.AddEndpointsApiExplorer();//

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();//
app.MapBlazorHub();//

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())//
{//
    Shop.Infrastructure.DependencyResolver.DependencyResolverService.MigrateDatabase(scope.ServiceProvider);//
}//

app.Run();
