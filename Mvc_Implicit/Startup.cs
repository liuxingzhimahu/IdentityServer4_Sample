using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mvc_Implicit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication(_ =>
            {
                _.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                _.DefaultChallengeScheme = "oidc";
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, _ =>
            {
                _.Cookie.Name = "mvc_implicit_cookie";
            })
            .AddOpenIdConnect("oidc", _ =>
            {
                _.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                _.Authority = "http://localhost:5000";
                _.RequireHttpsMetadata = false;
                _.ClientId = "mvc_implicit";
                _.ClientSecret = "secret";
                _.SaveTokens = true;
            })
            .AddGithub();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
