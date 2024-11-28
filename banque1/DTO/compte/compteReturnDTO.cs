using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.compte
{
    public class compteReturnDTO
    {
        public string Id { get; set; } = null!;

        public string NumeroCompte { get; set; } = null!;

        public string Nom { get; set; } = null!;

        public string Postnom { get; set; } = null!;

        public string Prenom { get; set; } = null!;

        public string Telephone { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Adresse { get; set; } = null!;

        public string Sexe { get; set; } = null!;

        public string Devise { get; set; } = null!;
    }
}
