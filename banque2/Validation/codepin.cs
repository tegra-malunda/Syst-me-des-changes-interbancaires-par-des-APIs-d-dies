namespace banque2.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class CodepinValidationAttribute : ValidationAttribute
    {
        // You can set a default error message or allow it to be overridden.
        public CodepinValidationAttribute()
            : base("Code pin doit avoir 4 chiffres") { }
         
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            string pin = value as string;
             
            if ( pin.Length!=4)
            {
                return false;
            }

             
            return true ;
        }
    }
}