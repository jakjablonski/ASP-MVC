using HurtowniaMVC.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class Czesc
    {
        
        public int CzescId { get; set; }
        public int KategoriaId { get; set; }
        [IsPartNumber]
        [Required(ErrorMessage = "Wprowadź nazwe")]
        [DisplayName("nazwa")]
        public string Nazwa { get; set; }
       [Required(ErrorMessage = "Wprowadź cene")]
        [DisplayName("cena")]
        public decimal Cena { get; set; }

        public virtual Kategoria Kategoria { get; set; }
    }
}