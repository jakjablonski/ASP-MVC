using HurtowniaMVC.Models;
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
            var czesci = new List<Czesc>
            {
                new Czesc { Nazwa = "Czesc1", Cena = 1 },
                new Czesc { Nazwa = "Czesc2", Cena = 2 },
                new Czesc { Nazwa = "Czesc3", Cena = 3 },
                new Czesc { Nazwa = "Czesc4", Cena = 4 },
                new Czesc { Nazwa = "Czesc5", Cena = 5 },

            };
            czesci.ForEach(cz => context.Czesc.Add(cz));
            context.SaveChanges();
        }
    }
}