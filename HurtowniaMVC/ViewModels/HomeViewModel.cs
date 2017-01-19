using HurtowniaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Kategoria> Kategorie { get; set; }
    }
}