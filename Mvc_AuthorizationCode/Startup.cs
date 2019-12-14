using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;

namespace Mvc_AuthorizationCode
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddControllersWithViews();
            services.AddAuthentication(_ =>
            {
                _.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                _.DefaultChallengeScheme = "oidc";
               //_.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, _ =>
            {
                _.Cookie.Name = "mvc_authorizationcode_cookie";
                _.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                _.Cookie.HttpOnly = true;
            })
            .AddOpenIdConnect("oidc", _ =>
            {
                //_.
                _.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                _.Authority = "http://localhost:5000";
                _.RequireHttpsMetadata = false;
                _.ClientId = "mvc_authorizationcode";
                _.ClientSecret = "secret";
                _.ResponseType = "code";
                _.SaveTokens = true;
               //_.Scope.Clear();
               //_.Scope.Add(OidcConstants.StandardScopes.OpenId);
               //_.Scope.Add(OidcConstants.StandardScopes.Profile);
               // _.Scope.Add(OidcConstants.StandardScopes.Email);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
