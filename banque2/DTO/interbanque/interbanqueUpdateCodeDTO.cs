using banque2.Validation;
using System.ComponentModel.DataAnnotations;

namespace banque2.DTO.interbanque
{
    public class InterbanqueUpdateCodeDTO
    {
        [Required]
        public string id { get; set; } = null!;
        [Required]
        public string Code_old { get; set; } = null!;
        [Required]
        public string Code_new { get; set; } = null!;
        
    }
}
