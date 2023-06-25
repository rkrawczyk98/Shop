using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces;
using Shop.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.DependencyResolver
{
    public static class DependencyResolverService
    {
        public static void Register(IServiceCollection services) 
        {
            services.AddHttpContextAccessor();
            //services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddHttpClient<IRoleService, RoleService>(); // nie wiem ktory i co
            services.AddHttpClient<IUserService, UserService>();
        }


    }
}
