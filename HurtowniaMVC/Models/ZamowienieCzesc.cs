using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class ZamowienieCzesc
    {
        public int ZamowienieCzescId { get; set; }
        public int ZamowienieId { get; set; }
        public int CzescId { get; set; }
        public int Ilosc { get; set; }
        public virtual Zamowienie Zamowienie { get; set; }
        public virtual Czesc Czesc { get; set; }
    }
}