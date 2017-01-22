using Hurtownia.Models;
using HurtowniaMVC.Models;
using HurtowniaMVC.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.DAL
{
    public class StoreContext : IdentityDbContext<ApplicationUser>
    {
        public StoreContext() : base("StoreContext")
        {

        }
        public static StoreContext Create()
        {
            return new StoreContext();
        }
        public DbSet<Kategoria> Kategoria { get; set; }
        public DbSet<Czesc> Czesc { get; set; }
        public DbSet<Zamowienie> Zamowienie { get; set; }
        public DbSet<ZamowienieCzesc> ZamowienieCzesc { get; set; }

    }
}