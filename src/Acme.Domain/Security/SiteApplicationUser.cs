namespace Acme.Domain.Security
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using EPiServer.Shell.Security;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class SiteApplicationUser : IdentityUser, IUIUser
    {
        public SiteApplicationUser()
        {
            this.CreationDate = DateTime.UtcNow;
        }

        [NotMapped]
        public string Username
        {
            get { return this.UserName; }
            set { this.UserName = value; }
        }

        public string Comment { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLoginDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastLockoutDate { get; set; }

        public string PasswordQuestion { get; set; }

        public string ProviderName => "Acme";
    }
}
