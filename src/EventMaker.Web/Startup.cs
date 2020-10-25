using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Managers;
using EventMaker.BLL.Mappings;
using EventMaker.BLL.Repository;
using EventMaker.DAL.Context;
using EventMaker.DAL.Entities;
using EventMaker.Web.Mappings;
using EventMaker.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EventMaker.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Managers
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IEventManager, EventManager>();

            // Database context
            services.AddDbContext<EventMakerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnection")));

            // Registering and Initializing AutoMapper Profiles
            services.AddAutoMapper(typeof(EventWebProfile));
            services.AddAutoMapper(typeof(EventProfile));

            // ASP.NET Core Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
            })
              .AddEntityFrameworkStores<EventMakerDbContext>();
            // Microsoft services
            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Middlewares
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
