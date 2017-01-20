using HurtowniaMVC.DAL;
using HurtowniaMVC.Infrastructure;
using HurtowniaMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HurtowniaMVC.Controllers
{
    public class CartController : Controller
    {
        private ShoppingCartManager shoppingCartManager;
        private ISessionManager sessionManager { get; set; }
        private StoreContext db = new StoreContext();
        public CartController()
        {
            this.sessionManager = new SessionManager();
            this.shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);
        }
        // GET: Cart
        public ActionResult Index()
        {
            var cartItems = shoppingCartManager.GetCart();
            var cartTotalPrice = shoppingCartManager.GetCartTotalPrice();
            CartViewModel cartViewModel = new CartViewModel() { CartItems = cartItems, TotalPrice = cartTotalPrice };
            return View(cartViewModel);
        }
        public ActionResult AddToCart(int id)
        {
            shoppingCartManager.AddToCart(id);
            return RedirectToAction("Index");
        }
        public decimal GetCartItemsPrice()
        {
            return shoppingCartManager.GetCartTotalPrice();
        }
        public decimal GetCartItemsCount()
        {
            return shoppingCartManager.GetCartItemsCount();
        }

        public ActionResult RemoveFromCart(int czescID)
        {
            ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);

            int itemCount = shoppingCartManager.RemoveFromCart(czescID);
            int cartItemsCount = shoppingCartManager.GetCartItemsCount();
            decimal cartTotal = shoppingCartManager.GetCartTotalPrice();

            // Return JSON to process it in JavaScript
            var result = new CartRemoveViewModel
            {
                RemoveItemId = czescID,
                RemovedItemCount = itemCount,
                CartTotal = cartTotal,
                CartItemsCount = cartItemsCount
            };

            return Json(result);
        }
    }
}