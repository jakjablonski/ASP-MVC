using HurtowniaMVC.Models;
using HurtowniaMVC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Hurtownia.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Zamowienie> Zamowienia { get; set; }

        public UserData UserData { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
  

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    static ApplicationDbContext()
    //    {
    //        // Set the database intializer which is run once during application start
    //        // This seeds the database with admin user credentials and admin role
    //        Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}