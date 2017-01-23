using HurtowniaMVC.DAL;
using HurtowniaMVC.Models;
using HurtowniaMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HurtowniaMVC.Infrastructure
{
    public class ShoppingCartManager
    {
        private StoreContext db;
        private ISessionManager session;

        public const string CartSessionKey = "CartData";
        public ShoppingCartManager(ISessionManager session, StoreContext db)
        {
            this.session = session;
            this.db = db;
        }
        public void AddToCart(int czescid)
        {
            var cart = this.GetCart();
            var cartItem = cart.Find(c => c.Czesc.CzescId == czescid);
            if (cartItem != null)
                cartItem.Quantity++;
            else
            {
                var czescToAdd = db.Czesc.Where(cz => cz.CzescId == czescid).Single();
                if(czescToAdd != null)
                {
                    var newCartItem = new CartItem()
                    {
                        Czesc = czescToAdd,
                        Quantity = 1,
                        TotalPrice = czescToAdd.Cena
                    };
                    cart.Add(newCartItem);
                }
            }
            session.Set(CartSessionKey, cart);
        }
        public List<CartItem> GetCart()
        {
            List<CartItem> cart;

            if (session.Get<List<CartItem>>(CartSessionKey) == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = session.Get<List<CartItem>>(CartSessionKey) as List<CartItem>;
            }

            return cart;
        }
        public int RemoveFromCart(int czescid)
        {
            var cart = this.GetCart();

            var cartItem = cart.Find(c => c.Czesc.CzescId == czescid);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    return cartItem.Quantity;
                }
                else
                    cart.Remove(cartItem);
            }

           
            return 0;
        }
        public decimal GetCartTotalPrice()
        {
            var cart = this.GetCart();
            return cart.Sum(c => (c.Quantity * c.Czesc.Cena));
        }

        public int GetCartItemsCount()
        {
            var cart = this.GetCart();
            int count = cart.Sum(c => c.Quantity);

            return count;
        }

        public Zamowienie CreateOrder(Zamowienie newZamowienie, string userId)
        {
            var cart = this.GetCart();

            newZamowienie.DataZamowienia = DateTime.Now;
            newZamowienie.UserId = userId;

            this.db.Zamowienie.Add(newZamowienie);

            if (newZamowienie.ZamowienieCzesc == null)
                newZamowienie.ZamowienieCzesc = new List<ZamowienieCzesc>();

            decimal cartTotal = 0;

            foreach (var cartItem in cart)
            {
                var newZamowienieCzesc = new ZamowienieCzesc()
                {
                    CzescId = cartItem.Czesc.CzescId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Czesc.Cena
                };

                cartTotal += (cartItem.Quantity * cartItem.Czesc.Cena);

                newZamowienie.ZamowienieCzesc.Add(newZamowienieCzesc);
            }

            newZamowienie.KwotaZamowienia = cartTotal;

            this.db.SaveChanges();

            return newZamowienie;
        }
        public void EmptyCart()
        {
            session.Set<List<CartItem>>(CartSessionKey, null);
        }
    }
}