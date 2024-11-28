namespace interbanque.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class SexeValidationAttribute : ValidationAttribute
    {
        // You can set a default error message or allow it to be overridden.
        public SexeValidationAttribute()
            : base("Sexe doit être M ou F") { }
         
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            string sexe = value as string;
             
            if ( !sexe.ToUpper().Equals("M") && !sexe.ToUpper().Equals("F"))
            {
                return false;
            }

             
            return true ;
        }
    }
}