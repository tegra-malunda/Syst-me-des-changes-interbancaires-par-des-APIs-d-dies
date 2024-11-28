using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.interbanque
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
