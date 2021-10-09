using CazareCamine.Data.Context;
using CazareCamine.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CazareCamine.Data
{
    public static class DataServiceExtension
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IRoleManager, RoleManager>();
            services.AddDbContext<UserContext>(o => {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
