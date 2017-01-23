using HurtowniaMVC.Validators;
using HurtowniaMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Models
{
    public class Kategoria
    {
        
        
        public int KategoriaId { get; set; }
        [isGood]

        public string Nazwa { get; set; }

        public  ICollection<Czesc> Czesci { get; set; }
    }
}