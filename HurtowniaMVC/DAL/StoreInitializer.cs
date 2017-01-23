using Hurtownia.Models;
using HurtowniaMVC.Models;
using HurtowniaMVC.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.DAL
{

    public class StoreInitializer : DropCreateDatabaseAlways<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            SeedStoreData(context);
            base.Seed(context);
        }

        private void SeedStoreData(StoreContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria {Nazwa = "Osobowe", KategoriaId = 1},
                new Kategoria {Nazwa = "Ciezarowe" , KategoriaId =2},
            };
            kategorie.ForEach(k => context.Kategoria.Add(k));
            context.SaveChanges();



            var czesci = new List<Czesc>
            {
                new Czesc { Nazwa = "Czesc1", Cena = 1 ,KategoriaId=1},
                new Czesc { Nazwa = "Czesc2", Cena = 2 ,KategoriaId=1},
                new Czesc { Nazwa = "Czesc3", Cena = 3 ,KategoriaId=1},
                new Czesc { Nazwa = "Czesc4", Cena = 4 ,KategoriaId=2},
                new Czesc { Nazwa = "Czesc5", Cena = 5 ,KategoriaId=2},
                new Czesc { Nazwa = "Czesc6", Cena = 6 ,KategoriaId=2},

            };
            czesci.ForEach(cz => context.Czesc.Add(cz));
            context.SaveChanges();
        }
        public static void InitializeIdentityForEF(StoreContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@admin.pl";
            const string password = "123";
            const string roleName = "Admin";


            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, UserData = new UserData() };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            //var user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}