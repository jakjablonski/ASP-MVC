using HurtowniaMVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HurtowniaMVC.Controllers
{
    public class StoreController : Controller
    {
        StoreContext db = new StoreContext();
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult List(string nazwakategori)
        {
            var kategoria = db.Kategoria.Include("Czesci").Where(g => g.Nazwa.ToUpper() == nazwakategori.ToUpper()).Single();
            var czesci = kategoria.Czesci.ToList();
            return View(czesci);
        }
      
        [ChildActionOnly]
        public ActionResult KategoriaMenu()
        {
            var kategoria = db.Kategoria.ToList();
            return PartialView("_KategoriaMenu", kategoria);
        }
    }
}