using Hurtownia.Models;
using HurtowniaMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class Zamowienie
    {
        public int ZamowienieId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        [DisplayName("Imie"), Required]
        public string Imie { get; set; }
        [DisplayName("Nazwisko"), Required]
        public string Nazwisko { get; set; }
        [DisplayName("Miastoo"), Required]
        public string Miasto { get; set; }
        [DisplayName("Adres"), Required]
        public string Adres { get; set; }

        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail."), Required]
        public string Email { get; set; }
        
        [RegularExpression(@"(\+\d{2})*[\d\s-]+",
             ErrorMessage = "Błędny format numeru telefonu."), Required]
        public string NumerTel { get; set; }
        public DateTime DataZamowienia { get; set; }
        

        public decimal KwotaZamowienia { get; set;}
        public List<ZamowienieCzesc> ZamowienieCzesc { get; set; }
    }
    
}