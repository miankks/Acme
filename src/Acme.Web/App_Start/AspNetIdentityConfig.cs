[assembly: Microsoft.Owin.OwinStartup(typeof(Acme.Web.AspNetIdentityConfig))]

namespace Acme.Web
{
    using System;
    using Acme.Domain.Security;
    using EPiServer.Cms.UI.AspNetIdentity;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;

    public class AspNetIdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            // Add CMS integration for ASP.NET Identity
            app.AddCmsAspNetIdentity<SiteApplicationUser>();

            // Use cookie authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/util/login.aspx"), // TODO: Change if you have a custom login page, or remove this comment.
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager<SiteApplicationUser>, SiteApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.GenerateUserIdentityAsync(user))
                }
            });
        }
    }
}
