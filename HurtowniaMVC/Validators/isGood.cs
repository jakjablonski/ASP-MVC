using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HurtowniaMVC.Validators
{
    public class isGood : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage;
            string numer;
            int x;
            if (value is string)
                numer = value.ToString();
            else
                return new ValidationResult("Rejs musi mieć numer");

            int dl = numer.Length;

            if (dl <3 && dl >10)
            {
                errorMessage = "nazwa musi skladać sie od 3 do 10 znakow";
                return new ValidationResult(errorMessage);
            }

           

            
            return ValidationResult.Success;
        }
    }
}