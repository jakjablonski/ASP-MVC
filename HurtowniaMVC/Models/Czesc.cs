﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class Czesc
    {
        public int CzescId { get; set; }
        public int KategoriaId { get; set; }
        public string Nazwa { get; set; }
        public decimal Cena { get; set; }
        public bool Dostepna { get; set; }

        public virtual Kategoria Kategoria { get; set; }
    }
}