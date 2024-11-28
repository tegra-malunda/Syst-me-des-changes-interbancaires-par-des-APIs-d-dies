namespace banque2.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class DeviseValidationAttribute : ValidationAttribute
    {
        // You can set a default error message or allow it to be overridden.
        public DeviseValidationAttribute()
            : base("Devise doit être CDF ou USD") { }
         
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            string devise = value as string;
             
            if ( !devise.ToUpper().Equals("CDF") && !devise.ToUpper().Equals("USD"))
            {
                return false;
            }

             
            return true ;
        }
    }
}