using HurtowniaMVC.Models;
using HurtowniaMVC.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.DAL
{
       
    public class StoreInitializer : DropCreateDatabaseIfModelChanges<StoreContext>
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
}
}