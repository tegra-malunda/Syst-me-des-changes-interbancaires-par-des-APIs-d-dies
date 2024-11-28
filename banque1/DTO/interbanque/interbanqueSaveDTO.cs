using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.interbanque
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
