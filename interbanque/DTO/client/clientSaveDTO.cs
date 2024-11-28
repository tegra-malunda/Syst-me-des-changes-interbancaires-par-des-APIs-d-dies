using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.compte
{
    public class clientSaveDTO
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

        [motpasseValidation]
        public string Motpasse { get; set; } = null!;
        [SexeValidation] 
        public string Sexe { get; set; } = null!; 
    }
}
