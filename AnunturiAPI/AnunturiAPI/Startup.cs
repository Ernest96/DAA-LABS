using System.Linq;
using System.Threading.Tasks;
using AnunturiAPI.Constants;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(AnunturiAPI.Startup))]

namespace AnunturiAPI
{
    public partial class Startup
    {
        public class OAuthBearerProvider : OAuthBearerAuthenticationProvider
        {
            public override Task RequestToken(OAuthRequestTokenContext context)
            {

                // Token is in context.Token

                var token = context.Request.Headers.Get("access_token");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Token = token;
                }

                var apikey = context.Request.Headers.Get("api-key");

                if (string.IsNullOrWhiteSpace(apikey) || apikey != APIEnvironment.ApiKey)
                {
                    context.Token = null;
                }

                return Task.FromResult<object>(null);
            }
        }

        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalAnunt", map =>
            {
                map.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
                map.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
                {
                    Provider = new OAuthBearerProvider()
                });

                var hubConfiguration = new HubConfiguration
                {
                    Resolver = GlobalHost.DependencyResolver,
                    EnableDetailedErrors = true,
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });

           
            ConfigureAuth(app);
            SeedRoles();
            RegisterAdmin();
        }

        private void SeedRoles()
        {
            var context = new ApplicationDbContext();

            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole { Name = RoleConsts.ADMIN });
                context.Roles.Add(new IdentityRole { Name = RoleConsts.MAI });
                context.Roles.Add(new IdentityRole { Name = RoleConsts.SIS });
                context.Roles.Add(new IdentityRole { Name = RoleConsts.FBR });

                context.SaveChanges();
            }
        }

        private void RegisterAdmin()
        {
            var dbContext = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext));

            if (userManager.FindByName(LoginConsts.Admin) == null)
            {
                var user = new ApplicationUser() { UserName = LoginConsts.Admin, Email = "abcode.moldova@gmail.com", EmailConfirmed = true };

                IdentityResult result = userManager.CreateAsync(user, "Qwerty2019").GetAwaiter().GetResult();
                var admin = userManager.FindByName(LoginConsts.Admin);
                userManager.AddToRole(admin.Id, RoleConsts.ADMIN);
            }
        }
    }
}
