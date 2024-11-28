using banque1.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque1.DTO.interbanque
{
    public class InterbanqueUpdateTokenDTO
    {
        [Required]
        public string id { get; set; } = null!;
        [Required]
        public string Token_old { get; set; } = null!;
        [Required]
        public string Token_new { get; set; } = null!;
        
    }
}
