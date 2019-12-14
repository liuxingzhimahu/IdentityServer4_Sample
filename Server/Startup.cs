using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Server
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging(log => log.AddConsole());

            services.AddRouting();

            services.AddControllersWithViews();

            services.AddIdentityServer()
                           .AddDeveloperSigningCredential()
                           .AddTestUsers(Config.Users().ToList())
                           .AddInMemoryApiResources(Config.GetApis())
                           .AddInMemoryClients(Config.GetClients())
                           .AddInMemoryIdentityResources(Config.GetIdentityResources());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
