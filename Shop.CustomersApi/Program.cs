using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shop.UsersApi;
using Shop.UsersApi.Data;
using Shop.UsersApi.Models;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

var key = Encoding.ASCII.GetBytes("abc12c04-83cd-4778-8715-72f3ec104006"); // Klucz tajny u¿ywany do podpisu i weryfikacji tokenu

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Ustawiæ na true w œrodowisku produkcyjnym
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false, // Jeœli u¿ywasz konkretnego wydawcy tokenu (issuer), ustaw wartoœæ true i okreœl prawid³owy issuer
        ValidateAudience = false, // Jeœli u¿ywasz konkretnego odbiorcy tokenu (audience), ustaw wartoœæ true i okreœl prawid³owy audience
    };
});
//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(x =>
//{
//    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        ValidIssuer = config["JwtSettings:Issuer"],
//        ValidAudience = config["JwtSettings:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey
//            (Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true
//    };
//});

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ShopDbContext>(options => 
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
    .AddEntityFrameworkStores<ShopDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options => 
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await SeedData.EnsureSeedData(scope, app.Configuration, app.Logger);
}

app.Run();
