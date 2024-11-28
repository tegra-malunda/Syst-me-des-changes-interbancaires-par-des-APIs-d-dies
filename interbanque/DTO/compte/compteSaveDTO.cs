using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.interbanque
{
    public class compteSaveDTO
    {
        [Required]
        public string IdClient { get; set; } = null!;
        [Required]
        public string IdBanque { get; set; } = null!;
        [Required]
        public string NumeroCompte { get; set; } = null!;
        [DeviseValidation]
        public string Devise { get; set; } = null!;

    }
}
