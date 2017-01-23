using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Validators
{
    public class IsPartNumber: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage;
            string nazwa;
            int x;


            if (value is string)
                nazwa = value.ToString();
            else
                return new ValidationResult("Czesc musi mieć odpowiednia nazwe!!");

            int dlugosc = nazwa.Length;

            if (dlugosc < 6)
            {
                errorMessage = "nazwa musi się skladac z wiecej niz 6 znaków";
                return new ValidationResult(errorMessage);
            }

            for (int i = 0; i < 5; i++)
                if (!Char.IsLetter(nazwa[i]))
                {
                    errorMessage = "nazwa musi byc w formacie  czesc[0-9][0-9][0-9][0-9]";
                    return new ValidationResult(errorMessage);
                }

            
            return ValidationResult.Success;
        }

    }
    
}