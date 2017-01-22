using HurtowniaMVC.ViewModels;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using HurtowniaMVC.DAL;
using HurtowniaMVC.App_Start;
using Hurtownia.Models;
using System.Collections.Generic;
using System.Data.Entity;
using HurtowniaMVC.Models;

namespace HurtowniaMVC.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        StoreContext db = new StoreContext();

        public ManageController()
        {
        }


        public ManageController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            //SignInManager = signInManager;
        }
        //private ApplicationSignInManager _signInManager;
        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            LinkSuccess,
            Error
        }

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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }


        // GET: Manage
        public async Task<ActionResult> Index()
        {
            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.Identity.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();

            var model = new ManageCredentialsViewModel
            {
                //Message = message,
                HasPassword = this.HasPassword(),
                CurrentLogins = userLogins,
                OtherLogins = otherLogins,
                ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1,
                UserData = user.UserData
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "UserData")]UserData userData)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.UserData = userData;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            // In case we have simple errors - return
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            // In case we have login errors
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword([Bind(Prefix = "SetPasswordViewModel")]SetPasswordViewModel model)
        {
            // In case we have simple errors - return
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {

                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);

                if (!ModelState.IsValid)
                {
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }
            }

            var message = ManageMessageId.SetPasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Index", new { Message = message });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }


        public ActionResult OrdersList()
        {
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Zamowienie> userZamowienie;

            // For admin users - return all orders
            if (isAdmin)
            {
                userZamowienie = db.Zamowienie.Include("ZamowienieCzesc").
                    OrderByDescending(o => o.DataZamowienia).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                userZamowienie = db.Zamowienie.Where(o => o.UserId == userId).Include("ZamowienieCzesc").
                    OrderByDescending(o => o.DataZamowienia).ToArray();
            }

            return View(userZamowienie);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public StanZamowienia ChangeStanZamowienia(Zamowienie zamowienie)
        {
            Zamowienie zamowienieToModify = db.Zamowienie.Find(zamowienie.ZamowienieId);
            zamowienieToModify.StanZamowienia = zamowienie.StanZamowienia;
            db.SaveChanges();

            if (zamowienieToModify.StanZamowienia == StanZamowienia.Shipped)
            {
                // Schedule confirmation
                //string url = Url.Action("SendStatusEmail", "Manage", new { orderid = orderToModify.OrderId, lastname = orderToModify.LastName }, Request.Url.Scheme);

                //BackgroundJob.Enqueue(() => Helpers.CallUrl(url));

                //IMailService mailService = new HangFirePostalMailService();
                //mailService.SendOrderShippedEmail(orderToModify);

                //mailService.SendOrderShippedEmail(orderToModify);

                //dynamic email = new Postal.Email("OrderShipped");
                //email.To = orderToModify.Email;
                //email.OrderId = orderToModify.OrderId;
                //email.FullAddress = string.Format("{0} {1}, {2}, {3}", orderToModify.FirstName, orderToModify.LastName, orderToModify.Address, orderToModify.CodeAndCity);
                //email.Send();
            }

            return zamowienie.StanZamowienia;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddProduct(int? czescId, bool? confirmSuccess)
        {
            if (czescId.HasValue)
                ViewBag.EditMode = true;
            else
                ViewBag.EditMode = false;

            var result = new EditProductViewModel();
            var kategoria = db.Kategoria.ToArray();
            result.Kategoria = kategoria;
            result.ConfirmSuccess = confirmSuccess;

            Czesc cz;

            if (!czescId.HasValue)
            {
                cz = new Czesc();
            }
            else
            {
                cz = db.Czesc.Find(czescId);
            }

            result.Czesc = cz;

            return View(result);
        }

        [HttpPost]
        public ActionResult AddProduct(HttpPostedFileBase file, EditProductViewModel model)
        {
            if (model.Czesc.CzescId > 0)
            {
                // Saving existing entry

                db.Entry(model.Czesc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AddProduct", new { confirmSuccess = true });
            }
            else
            {
                // Creating new entry

                var f = Request.Form;
                // Verify that the user selected a file
                if (file != null && file.ContentLength > 0)
                {
                    // Generate filename

                    //var fileExt = Path.GetExtension(file.FileName);
                    //var filename = Guid.NewGuid() + fileExt;

                    //var path = Path.Combine(Server.MapPath(AppConfig.PhotosFolderRelative), filename);
                    //file.SaveAs(path);

                    //// Save info to DB
                    //model.Album.CoverFileName = filename;
                    //model.Album.DateAdded = DateTime.Now;

                    //db.Entry(model.Album).State = EntityState.Added;
                    //db.SaveChanges();

                    return RedirectToAction("AddProduct", new { confirmSuccess = true });
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku.");
                    var kategoria = db.Kategoria.ToArray();
                    model.Kategoria = kategoria;
                    return View(model);
                }
            }

        }

        //public ActionResult HideProduct(int czescId)
        //{
        //    var album = db.Czesc.Find(czescId);
        //    czescId.IsHidden = true;
        //    db.SaveChanges();

        //    return RedirectToAction("AddProduct", new { confirmSuccess = true });
        //}

        //public ActionResult UnhideProduct(int albumId)
        //{
        //    var album = db.Albums.Find(albumId);
        //    album.IsHidden = false;
        //    db.SaveChanges();

        //    return RedirectToAction("AddProduct", new { confirmSuccess = true });
        //}

    }
}