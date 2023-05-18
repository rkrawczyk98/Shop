using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Interfaces;
using Shop.Infrastructure.Services;
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
            services.AddScoped<IAccountService, AccountService>();
        }


    }
}
