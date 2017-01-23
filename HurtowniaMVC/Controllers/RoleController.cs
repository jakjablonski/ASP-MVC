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
        
    }
}