using HurtowniaMVC.DAL;
using HurtowniaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using HurtowniaMVC.App_Start;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;
using System.Data;

namespace HurtowniaMVC.Controllers
{
    public class AdminController : Controller
    {

        private StoreContext db = new StoreContext();


        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }
        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { _roleManager = value; }
        }





        // GET: /Admin/
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            if (userID != null)
            {
                ViewBag.userAdmin = UserManager.IsInRole(userID, "Admin");
                ViewBag.userUzytkownik = UserManager.IsInRole(userID, "Uzytkownik");
            }
            return View(db.Czesc.ToList());
        }

        // GET: /Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czesc czesc = db.Czesc.Find(id);
            if (czesc == null)
            {
                return HttpNotFound();
            }
            var kategoriaId = czesc.KategoriaId;
            Kategoria kategoria = db.Kategoria.Find(kategoriaId);
            ViewBag.kategoriaName = kategoria.Nazwa;
            return View(czesc);
        }

        // GET: /Admin/Create
        public ActionResult Create()
        {
            Czesc czesc = new Czesc();
            ViewBag.KategoriaId = new SelectList(db.Kategoria, "kategoriaId", "Nazwa", czesc.KategoriaId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CzescId, KategoriaId,Cena,Nazwa")] Czesc czesc)
        {
            if (ModelState.IsValid)
            {
                db.Czesc.Add(czesc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriaId = new SelectList(db.Kategoria, "kategoriaId", "nazwa", czesc.KategoriaId);
            return View(czesc);
        }

        // GET: /Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czesc czesc = db.Czesc.Find(id);
            if (czesc == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriaId = new SelectList(db.Kategoria, "KategoriaId", "Nazwa", czesc.KategoriaId);
            return View(czesc);
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CzescId,Nazwa,Cena,KategoriaId")] Czesc czesc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(czesc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriaId = new SelectList(db.Kategoria, "KategoriaId", "Nazwa", czesc.KategoriaId);
            return View(czesc);
        }

        // GET: /Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Czesc czesc = db.Czesc.Find(id);
            if (czesc == null)
            {
                return HttpNotFound();
            }
            return View(czesc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Czesc czesc = db.Czesc.Find(id);
            db.Czesc.Remove(czesc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
