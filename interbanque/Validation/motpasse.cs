namespace interbanque.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class motpasseValidationAttribute : ValidationAttribute
    {
        // You can set a default error message or allow it to be overridden.
        public motpasseValidationAttribute()
            : base("Le mot de passe doit avoir au moins 8 caractères") { }
         
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            string motpasse = value as string;
             
            if (motpasse.Length<8)
            {
                return false;
            }

             
            return true ;
        }
    }
}