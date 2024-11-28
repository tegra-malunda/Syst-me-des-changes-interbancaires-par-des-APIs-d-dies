using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.interbanque
{
    public class interbanqueSaveDTO
    {
        
        [Required]
        public string Code { get; set; } = null!;
        [Required]
        public string Nom { get; set; } = null!;
        [Required]
        public string Token { get; set; } = null!;
    }
}
