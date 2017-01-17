using HurtowniaMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.DAL
{
    public class StoreContext : DbContext
    {
        public DbSet<Czesc> Czesc { get; set; }
        public DbSet<Zamowienie> Zamowienie { get; set; }
        public DbSet<ZamowienieCzesc> ZamowienieCzesc { get; set; }

    }
}