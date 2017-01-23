using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using HurtowniaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HurtowniaMVC.DAL;
using Hurtownia.Models;
using HurtowniaMVC.App_Start;
using Microsoft.AspNet.Identity.Owin;

namespace HurtowniaMVC.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        StoreContext db = new StoreContext();

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



        public ActionResult Details(string id)
        {

            return View(id);
        }

        public ActionResult Index()
        {
            var users = UserManager.Users.ToList();
            var roles = new List<Models.Roles>();

            foreach (var user in users)
            {
                var r = new Models.Roles
                {
                    UserName = user.UserName,
                    UserId = user.Id
                };
                roles.Add(r);
            }
            
            foreach (var user in roles)
            {
                user.RoleNames = UserManager.GetRoles(UserManager.Users.First(s => s.UserName == user.UserName).Id);
            }
            return View(roles);

            
        }

        public ActionResult Edit(string id)
        {
           
            var userName = UserManager.FindById(id);
            ViewBag.user = userName.UserName;
            bool rola = UserManager.IsInRole(id, "Uzytkownik");
            if (rola == true) { ViewBag.role = "Uzytkownik"; }
            else { ViewBag.role = "Admin"; }
            var roles = RoleManager.Roles.ToList();
            ViewBag.userroles = new SelectList(roles, "Id", "Name");
            return View();
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(string id, FormCollection Form)
        {
            
            string oldRoleId = "";
            string newRoleId = Request.Form["userroles"];
            bool rola = UserManager.IsInRole(id, "Uzytkownik");
            if (newRoleId == "1") { newRoleId = "Admin"; }
            else { newRoleId = "Uzytkownik"; }
            if (rola == true) { oldRoleId = "Uzytkownik"; } else { oldRoleId = "Admin"; }
            if (newRoleId == oldRoleId)
            {
                return RedirectToAction("Index");
            }
            else
            {
                UserManager.RemoveFromRole(id, oldRoleId);
                UserManager.AddToRole(id, newRoleId);
                return RedirectToAction("Index");
            }
        }

    }
}