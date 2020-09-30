using EventMaker.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventMaker.DAL.Extensions
{
    public static class ApplicationServiceCollectionExtension 
    {
        /// <summary>
        /// Dependency Injection.
        /// </summary>
        /// <param name="services"> Service collection. </param>
        /// <param name="configuration"> Configuration. </param>
        /// <returns></returns>
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventMakerDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQLConnection")));
            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<EventMakerDbContext>();

            return services;
        }
    }
}
