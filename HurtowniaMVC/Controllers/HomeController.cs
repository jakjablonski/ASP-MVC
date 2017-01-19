using HurtowniaMVC.DAL;
using HurtowniaMVC.Models;
using HurtowniaMVC.ViewModels;
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
            var kategorie = db.Kategoria;
            var vm = new HomeViewModel()
            {
                Kategorie = kategorie
            };
            return View(vm);
        }

        public ActionResult StaticContent(string viewname)
        {
            return View(viewname);
        }
    }
}