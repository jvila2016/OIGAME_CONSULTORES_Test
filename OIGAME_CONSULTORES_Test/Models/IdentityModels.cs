﻿using System.Data.Entity;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OIGAME_CONSULTORES_Test.Models.Security;

namespace OIGAME_CONSULTORES_Test.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static SqlConnection connection = new SqlConnection();
        public ApplicationDbContext()
           : base("ModelOIGAMECONSULTORESTest", throwIfV1Schema: false)
            //: base(connection.ConnectionString = Encr_Decr.Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["ModelOIGAMECONSULTORESTest"].ConnectionString), throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}