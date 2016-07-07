using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Cors;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Crossover.AirTicket.Api.Providers;
using Crossover.AirTicket.Api.Models;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.Facebook;

namespace Crossover.AirTicket.Api
{
    public class AirTicketCorsPolicy : ICorsPolicyProvider
    {
        private readonly CorsPolicy _corsPolicy;

        public AirTicketCorsPolicy()
        {
            _corsPolicy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true,
                AllowAnyOrigin = true
                
            };
            _corsPolicy.Origins.Add("http://localhost:53823");
        }

        public Task<CorsPolicy> GetCorsPolicyAsync(IOwinRequest request)
        {
            return Task.FromResult(_corsPolicy);
        }
    }

    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
          
            //app.UseCors(CorsOptions.AllowAll);
            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");
            var facebookOptions = new FacebookAuthenticationOptions();
            facebookOptions.AppId="244935575899284";
            facebookOptions.AppSecret = "39236e354bc4258d0ba5a9117c98d84d";
            facebookOptions.Scope.Add("email");
            facebookOptions.Scope.Add("public_profile");
            app.UseFacebookAuthentication(facebookOptions);

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }
}
