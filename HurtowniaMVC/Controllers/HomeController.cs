using HurtowniaMVC.DAL;
using HurtowniaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HurtowniaMVC.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext db = new StoreContext();
        
        // GET: Home
        public ActionResult Index()
        {
            Czesc newCzesc = new Czesc { Nazwa = "Czesc", Cena = 10, Dostepna = true };
            db.Czesc.Add(newCzesc);
            db.SaveChanges();
            return View();
        }
    }
}