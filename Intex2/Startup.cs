using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Intex2.Models;
using Microsoft.EntityFrameworkCore;

namespace Intex2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddDbContext<FagElGamousContext>(opts =>
                opts.UseSqlServer(Configuration[
                    "ConnectionStrings:EgyptConnection"]));

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = "545269694418-0aj3phfni9pv1mbpsstptmrbtg8qgj0l.apps.googleusercontent.com";
                options.ClientSecret = "KI0GGc875Dw4uDUlepGFWAn-";
                options.SignInScheme = IdentityConstants.ExternalScheme;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admins"));

                options.AddPolicy("RequireResearcherRole", policy => policy.RequireRole("Researcher"));
            });

            services.AddDbContext<EgyptContext>(opts =>
               opts.UseSqlServer(Configuration[
                   "ConnectionStrings:EgyptConnection"]));

            services.AddDbContext<IdentityContext>(opts =>
               opts.UseSqlServer(Configuration[
                   "ConnectionStrings:IdentityConnection"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.Configure<IdentityOptions>(opts => {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ12345678890@.";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            //Protects against Cross Site Scripting (Xss)
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                await next();
            });

            ////Add Content Security Policy (CSP)
            //app.Use(async (ctx, next) =>
            //{
            //    ctx.Response.Headers.Add("Content-Security-Policy",
            //    "default-src 'self'");
            //    await next();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "create",
                    pattern: "{controller=BurialCrud}/{action=Create}/{id?}");

                endpoints.MapControllerRoute(
                   "pagination",
                   "BurialCrud/{pageNum}",
                   new { Controller = "BurialCrud", action = "Index", pageNum = 1 });
            });

           // IdentitySeedData.CreateAdminAccount(app.ApplicationServices, Configuration);
        }
    }
}
