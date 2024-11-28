using interbanque.Validation;
using System.ComponentModel.DataAnnotations;

namespace interbanque.DTO.interbanque
{
    public class transfertSaveDTO
    {
        [Required]
        public string FromIdCompte { get; set; } = null!;
        [Required]
        public string ToIdBanque { get; set; } = null!;
        [Required]
        public string ToNumeroCompte { get; set; } = null!;
        [Required] 
        public decimal Montant { get; set; }

        

    }
}
