namespace Acme.Domain.Security
{
    using System.Data.Entity;
    using System.Threading.Tasks;
    using EPiServer.Cms.UI.AspNetIdentity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class CreateAdminUser : CreateDatabaseIfNotExists<ApplicationDbContext<SiteApplicationUser>>
    {
        protected override void Seed(ApplicationDbContext<SiteApplicationUser> context)
        {
            base.Seed(context);

            var userManager = new ApplicationUserManager<SiteApplicationUser>(new UserStore<SiteApplicationUser>(context));
            var roleManager = new ApplicationRoleManager<SiteApplicationUser>(new RoleStore<IdentityRole>(context));

            Task.Run(() => roleManager.CreateAsync(new IdentityRole("WebEditors"))).Wait();
            Task.Run(() => roleManager.CreateAsync(new IdentityRole("WebAdmins"))).Wait();

            Task.Run(() => userManager.CreateAsync(
                new SiteApplicationUser
                {
                    Email = "none@netrelations.com",
                    EmailConfirmed = true,
                    IsApproved = true,
                    UserName = "netrelations"
                },
                "4KEysitL")).Wait();

            var user = Task.Run(() => userManager.FindByNameAsync("netrelations")).Result;

            Task.Run(() => userManager.AddToRolesAsync(user.Id, "WebEditors", "WebAdmins")).Wait();
        }
    }
}
