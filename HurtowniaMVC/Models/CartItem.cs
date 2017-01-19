using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class CartItem
    {
        public Czesc Czesc { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}