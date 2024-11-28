using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.compte
{
    public class compteSaveDTO
    {
        [Required]
        public string Nom { get; set; } = null!;
        [Required]
        public string Postnom { get; set; } = null!;
        [Required]
        public string Prenom { get; set; } = null!;
        [Phone]
        public string Telephone { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Adresse { get; set; } = null!;
        [SexeValidation]
        public string Sexe { get; set; } = null!;
        [DeviseValidation]
        public string Devise { get; set; } = null!;
    }
}
