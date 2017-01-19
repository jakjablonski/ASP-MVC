using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class Zamowienie
    {
        public int ZamowienieId { get; set; }
        public string UserId { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Email { get; set; }
        public string NumerTel { get; set; }
        public DateTime DataZamowienia { get; set; }
        public StanZamowienia StanZamowienia { get; set; }

        public decimal KwotaZamowienia { get; set;}
        public List<ZamowienieCzesc> ZamowienieCzesc { get; set; }
    }
    public enum StanZamowienia
    {
        Nowe,
        Wyslane
    }
}