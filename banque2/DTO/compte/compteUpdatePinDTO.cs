using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.compte
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
