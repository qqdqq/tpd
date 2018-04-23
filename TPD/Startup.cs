using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TPD.Authorization;
using TPD.Data;
using TPD.Services;

namespace TPD
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var skipHTTPS = Configuration.GetValue<bool>("LocalTest:skipHTTPS");
            services.Configure<MvcOptions>(options =>
                {
                    if(Environment.IsDevelopment() && !skipHTTPS)
                    {
                        options.Filters.Add(new RequireHttpsAttribute());
                    }
                }
            );

            services.AddMvc();
            /*
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeFolder("/DailyReports");
                    options.Conventions.AuthorizeFolder("/ParkInformation").AllowAnonymousToPage("/ParkInformation/Index");
                    options.Conventions.AuthorizeFolder("/Account/Manage");
                    options.Conventions.AuthorizePage("/Account/Logout");
                });
            */
            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddMvc(config =>
               {
                   var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                   config.Filters.Add(new AuthorizeFilter(policy));
               }

            );

            services.AddSingleton<IAuthorizationHandler,
                                  AttractionAdministratorAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler,
                                  AttractionManagerAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
