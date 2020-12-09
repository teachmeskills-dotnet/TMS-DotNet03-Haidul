using System;
using AutoMapper;
using EventMaker.BLL.Interfaces;
using EventMaker.BLL.Managers;
using EventMaker.BLL.Mappings;
using EventMaker.BLL.Repository;
using EventMaker.BLL.Services;
using EventMaker.DAL.Context;
using EventMaker.DAL.Entities;
using EventMaker.Web.Mappings;
using EventMaker.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
            services.AddScoped(typeof(IRepository<>), typeof(RepositoryManager<>));
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IEventManager, EventManager>();
            services.AddScoped<IProfileManager, ProfileManager>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFiltrationService, FiltrationService>();
            services.AddScoped<IChatManager, ChatManager>();
            services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
            });

            // Database context
            services.AddDbContext<EventMakerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MSSQLConnection")));

            // ASP.NET Core Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = true;
            })
              .AddEntityFrameworkStores<EventMakerDbContext>()
              .AddDefaultTokenProviders();

            // Microsoft services
            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            // NuGet Services
            // Registering and Initializing AutoMapper Profiles
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EventWebProfile());
                mc.AddProfile(new EventProfile());
                mc.AddProfile(new CommentProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }

        public void Configure(IApplicationBuilder app)
        {
            //app.UseDeveloperExceptionPage();

            app.UseSerilogRequestLogging();

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

                endpoints.MapHub<EventChatHub>("/chat/{eventId}");
            });
        }
    }
}
