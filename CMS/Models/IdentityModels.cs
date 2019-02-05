using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CMS.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(128)]
        public string Avatar { get; set; }
        [StringLength(15)]
        public string Mobile { get; set; }
        public bool MobileConfirmed { get; set; }
        public DateTime? LastMobConfRequest { get; set; }
        public DateTime RegDateEn { get; set; }

        [StringLength(30)]
        public string LastLoginDate{ get; set; }
        public bool IsBanned { get; set; }
        [StringLength(100)]
        public string BannedMsg { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string About { get; set; }
        [StringLength(128)]
        public string MoarefID { get; set; }
        public int Money { get; set; }
        public short AccountType { get; set; }
        public DateTime? AccountExpireDateEn { get; set; }
        public bool AutoUpgrade { get; set; }




        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            //using (ServiceLayer.UserService service = new ServiceLayer.UserService())
            //{
            //    ViewModels.IdentityStoreViewModel tmp = service.GetIdentityInfo(userIdentity.GetUserId());


            //    userIdentity.AddClaim(new Claim("FullName", tmp.FullName.ToString()));
            //    userIdentity.AddClaim(new Claim("AccountType", tmp.AccountType.ToString()));
            //}

            userIdentity.AddClaim(new Claim("FullName", this.FullName?.ToString()));
            //userIdentity.AddClaim(new Claim("Avatar", this.Avatar?.ToString()));
            //userIdentity.AddClaim(new Claim("AccountType", this.AccountType.ToString()));
          

            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("CMSConnectionString", throwIfV1Schema: false)

        {
        }

        static ApplicationDbContext()
        {
            // Set the database intializer which is run once during application start
            // This seeds the database with admin user credentials and admin role
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!


            modelBuilder.Entity<ApplicationUser>().ToTable("UsersData");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
             


        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}