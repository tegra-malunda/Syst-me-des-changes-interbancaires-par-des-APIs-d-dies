using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.compte
{
    public class compteUpdatePinDTO
    {
        [Required]
        public string Numero_compte { get; set; } = null!;
        [CodepinValidation]
        public string Code_pin_old { get; set; } = null!;
        [CodepinValidation]
        public string Code_pin_new { get; set; } = null!;

    }
}
