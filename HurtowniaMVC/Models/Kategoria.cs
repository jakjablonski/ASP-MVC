using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class Kategoria
    {
        public int KategoriaId { get; set; }
        public string Nazwa { get; set; }

        public  ICollection<Czesc> Czesci { get; set; }
    }
}